﻿using RuinsOfAlbertrizal.AIs;
using RuinsOfAlbertrizal.Exceptions;
using RuinsOfAlbertrizal.Items;
using RuinsOfAlbertrizal.Mechanics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using System.Drawing;
using RuinsOfAlbertrizal.Environment;
using System.Xaml.Schema;

namespace RuinsOfAlbertrizal.Characters
{

    public abstract class Character : CharacterMapBasedObject, IRoundBasedObject, INotifyPropertyChanged
    {
        public int MovementPoints { get; set; }

        public int TurnsSinceLastAttack { get; set; }

        public const int MaxTurns = 2;

        public Point MapLocation { get; set; }

        public Point BattleFieldLocation { get; set; }

        public enum CardinalOrientation
        {
            North, East, South, West
        }

        public enum LeftRightOrientation
        {
            Left, Right
        }

        public CardinalOrientation AdventureOrientation { get; set; }

        public LeftRightOrientation BattleFieldOrientation { get; set; }

        public int TurnsPassed { get; set; }

        private int turnTicks;

        /// <summary>
        /// Once this value reaches the maximum speed, this character will start a round.
        /// </summary>
        public int TurnTicks
        {
            get => turnTicks;
            set
            {
                turnTicks = value;
                OnPropertyChanged("TurnTicks");
            }
        }

        public static int MaxTicks
        {
            get
            {
                if (GameBase.CurrentGame.CurrentBattleField != null)
                    return GameBase.CurrentGame.CurrentBattleField.MaxTicks;
                else
                    return -1;
            }
        }

        public override string DisplayName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(GeneralName))
                    return Name;
                else
                    return $"{Name} the {GeneralName}";
            }
        }

        /// <summary>
        /// The species name, such as human or slime
        /// </summary>
        public string GeneralName { get; set; }

        public int Level { get; set; }

        public enum ModelStyle
        {
            [Description("Armor and weapons are shown. The world image should be a multiple of [x] by [y] pixels. See help page for more information.")]
            Humanoid,
            [Description("Helmet and weapons are shown. The world image should be a multiple of [x] by [y] pixels. See help page for more information.")]
            Slime,
            [Description("No armor or weapons are shown.")]
            Other
        }

        public ModelStyle StyleOfModel { get; set; }

        private AI.AIStyle aiStyle;

        public virtual AI.AIStyle AIStyle
        {
            get => aiStyle;
            set
            {
                if (value == AI.AIStyle.NoChange)
                    return;

                aiStyle = value;
            }
        }

        [XmlIgnore]
        public Hitbox Hitbox
        {
            get
            {
                try
                {
                    return new Hitbox(WorldImg.Width, WorldImg.Height);
                }
                catch (ArgumentNullException)
                {
                    return new Hitbox();
                }
            }
        }

        /// <summary>
        /// The original stats that SHOULD NOT CHANGE.
        /// </summary>
        public int[] BaseStats { get; set; }

        [XmlIgnore]
        public int[] LeveledStats
        {
            get
            {
                int[] leveledStats = (int[])BaseStats.Clone();

                for (int i = 0; i < leveledStats.Length; i++)
                {
                    leveledStats[i] = (int)Math.Round(leveledStats[i] * (1 + (0.02 * (Level - 1))));
                }

                return leveledStats;
            }
        }

        /// <summary>
        /// Stats with the current armor equipted. Can be considered the max stats.
        /// </summary>
        [XmlIgnore]
        public int[] ArmoredStats
        {
            get
            {
                int[] armoredStats = new int[GameBase.NumStats];

                armoredStats = ArrayMethods.AddArrays(armoredStats, LeveledStats);

                foreach (Equiptment equiptment in CurrentEquiptments)
                {
                    if (equiptment == null || equiptment.IsAClone)
                        continue;

                    armoredStats = ArrayMethods.AddArrays(armoredStats, equiptment.StatGain);
                }

                return armoredStats;
            }
        }

        private int[] appliedStats = new int[GameBase.NumStats];

        /// <summary>
        /// Use this to apply damage or regen stats. 
        /// </summary>
        public int[] AppliedStats
        {
            get => appliedStats;
            set
            {
                if (!GameBase.Initialized())
                    return;

                //If invunerable, do not apply stats.
                if (IsInvunerable())
                    return;

                //value = CapAppliedStats(value);
                appliedStats = value;

                CheckIfDead();
            }
        }

        ///// <summary>
        ///// Caps the given 10 element array to the leveled stats.
        ///// </summary>
        ///// <param name="arr"></param>
        ///// <returns></returns>
        //private int[] CapAppliedStats(int[] arr)
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //        if (arr[i] > LeveledStats[i])
        //            arr[i] = LeveledStats[i];
        //        else if (arr[i] < 0)
        //            arr[i] = 0;
        //    }

        //    return arr;
        //}

        /// <summary>
        /// The stats from armor and buffs
        /// </summary>
        [XmlIgnore]
        public int[] ArmorAndBuffStats
        {
            get
            {
                int[] armorAndBuffStats = new int[GameBase.NumStats];

                armorAndBuffStats = ArrayMethods.AddArrays(armorAndBuffStats, ArmoredStats);

                foreach (Buff buff in CurrentBuffs)
                {
                    if (buff == null)
                        continue;

                    armorAndBuffStats = ArrayMethods.AddArrays(armorAndBuffStats, buff.LeveledStatGain);
                }

                return armorAndBuffStats;
            }
        }

        [XmlIgnore]
        public int[] CurrentStats
        {
            get
            {
                int[] currentStats = new int[GameBase.NumStats];

                currentStats = ArrayMethods.AddArrays(currentStats, ArmorAndBuffStats);
                currentStats = ArrayMethods.AddArrays(currentStats, AppliedStats);

                return currentStats;
            }
        }

        [XmlIgnore]
        public double[] PercentStats
        {
            get
            {
                double[] percentStats = new double[GameBase.NumStats];

                for (int i = 0; i < GameBase.NumStats; i++)
                {
                    percentStats[i] = CurrentStats[i] / (double)ArmoredStats[i];
                }

                return percentStats;
            }
        }

        public BuffGuidStorage PersonalPermanentBuffStorage { get; set; }

        /// <summary>
        /// This character will receive the following buffs.
        /// </summary>
        [XmlIgnore]
        public List<Buff> PersonalPermanentBuffs { get; set; }

        [XmlIgnore]
        public List<Buff> ArmoredBuffs
        {
            get
            {
                List<Buff> buffs = new List<Buff>();

                foreach (Equiptment equiptment in CurrentEquiptments)
                {
                    if (equiptment == null || equiptment.IsAClone)
                        continue;

                    buffs.AddRange(equiptment.GrantedBuffs);
                }

                return buffs;
            }
        }

        [XmlIgnore]
        public List<Buff> AllPermanentBuffs
        {
            get
            {
                List<Buff> permanentBuffs = new List<Buff>();

                permanentBuffs.AddRange(ArmoredBuffs);
                permanentBuffs.AddRange(PersonalPermanentBuffs);

                return permanentBuffs;
            }
        }

        public BuffGuidStorage PersonalBuffImmunityStorage { get; set; }

        /// <summary>
        /// This character is immune to the following buffs of the same name. Permanent buffs are ignored.
        /// </summary>
        [XmlIgnore]
        public List<Buff> PersonalBuffImmunities { get; set; }

        [XmlIgnore]
        public List<Buff> AllBuffImmunities
        {
            get
            {
                List<Buff> buffImmunities = new List<Buff>();

                foreach (Equiptment equiptment in CurrentEquiptments)
                {
                    if (equiptment == null || equiptment.IsAClone)
                        continue;

                    buffImmunities.AddRange(equiptment.BuffImmunities);
                }

                buffImmunities.AddRange(PersonalBuffImmunities);

                return buffImmunities;
            }
        }

        private List<Buff> appliedBuffs = new List<Buff>();

        /// <summary>
        /// Use this to directly apply buffs. These buffs will expire.
        /// </summary>
        public List<Buff> AppliedBuffs
        {
            get => appliedBuffs;
            set
            {
                if (!GameBase.Initialized())
                    return;

                //Do not allow buffs to be applied if player is invunerable
                if (IsInvunerable())
                    return;

                appliedBuffs = value;
            }
        }

        [XmlIgnore]
        public List<Buff> AppliedBuffsWithImmunities
        {
            get
            {
                List<Buff> buffs = new List<Buff>();
                buffs.AddRange(AppliedBuffs);

                foreach (Buff buff in AllBuffImmunities)
                {
                    buffs.RemoveAll(item => buff.HasSameGlobalIDAs(item));
                }

                return buffs;
            }
        }

        [XmlIgnore]
        public List<Buff> ConsumableBuffsWithImmunities
        {
            get
            {
                List<Buff> buffs = new List<Buff>();

                foreach (Consumable consumable in CurrentConsumables)
                {
                    buffs.AddRange(consumable.Buffs);
                }

                foreach (Buff buff in AllBuffImmunities)
                {
                    buffs.RemoveAll(item => buff.HasSameGlobalIDAs(item));
                }

                return buffs;
            }
        }

        [XmlIgnore]
        public List<Buff> CurrentBuffs
        {
            get
            {
                List<Buff> currentBuffs = new List<Buff>();
                foreach (Buff buff in AppliedBuffs)
                {
                    currentBuffs.Add(buff);
                }

                foreach (Consumable consumable in CurrentConsumables)
                {
                    currentBuffs.AddRange(consumable.Buffs);
                }

                foreach (Buff buff in AllBuffImmunities)
                {
                    currentBuffs.RemoveAll(item => buff.HasSameGlobalIDAs(item));
                }

                foreach (Buff buff in AllPermanentBuffs)
                {
                    currentBuffs.Add(buff);
                }

                return currentBuffs;
            }
        }

        [XmlIgnore]
        /// <summary>
        /// How strong the character is. Ignores any non-static buffs.
        /// </summary>
        public int BattleIndex
        {
            get
            {
                if (this == null)
                    return 0;

                int total = 0;

                foreach (int i in ArmoredStats)
                {
                    total += i;
                }

                return total;
            }
        }

        //public Guid[] CurrentEquiptmentGuids { get; set; }

        private Equiptment[] currentEquiptment;

        /// <summary>
        /// The equiptment the character has equipted
        /// </summary>
        public Equiptment[] CurrentEquiptments
        {
            get => currentEquiptment;
            set
            {
                currentEquiptment = value;
                OnPropertyChanged();
            }
        }

        public List<Guid> CurrentConsumableGuids { get; set; }

        private List<Consumable> currentConsumables;

        /// <summary>
        /// The consumables the character has consumed
        /// </summary>
        [XmlIgnore]
        public List<Consumable> CurrentConsumables
        {
            get => currentConsumables;
            set
            {
                currentConsumables = value;
                OnPropertyChanged();
            }
        }

        public List<Guid> BoundAttackGuids { get; set; }

        /// <summary>
        /// Attacks bound to the character
        /// </summary>
        [XmlIgnore]
        public List<Attack> BoundAttacks { get; set; }

        [XmlIgnore]
        public List<Attack> AllAttacks
        {
            get
            {
                List<Attack> attacks = new List<Attack>();
                attacks.AddRange(BoundAttacks);

                foreach (Equiptment equiptment in Equiptment.GetAttackableEquiptment(CurrentEquiptments.ToList()))
                {
                    if (equiptment == null || equiptment.IsAClone)
                        continue;

                    attacks.AddRange(equiptment.Attacks);
                }

                return attacks;
            }
        }

        /// <summary>
        /// True if character is charging an attack, false otherwise.
        /// </summary>
        [XmlIgnore]
        public bool IsCharging { get => AttackToCharge != null && CharacterTargetedByCharge != null && !CharacterTargetedByCharge.IsDead; }

        public Attack AttackToCharge { get; set; }

        public Character CharacterTargetedByCharge { get; set; }

        [XmlIgnore]
        public bool IsDead { get => CurrentStats[0] <= 0; }

        //public List<Character> PreviousTargets { get; set; }

        public Character() : base()
        {
            Level = 1;
            CurrentEquiptments = new Equiptment[GameBase.NumCurrentEquiptment];
            //CurrentEquiptmentGuids = new Guid[GameBase.NumCurrentEquiptment];
            CurrentConsumables = new List<Consumable>();
            PersonalBuffImmunities = new List<Buff>();
            PersonalPermanentBuffs = new List<Buff>();
            PersonalBuffImmunityStorage = new BuffGuidStorage();
            PersonalPermanentBuffStorage = new BuffGuidStorage();
            BaseStats = new int[GameBase.NumStats];
            MapLocation = new Point();
            BattleFieldLocation = new Point();
        }

        public override void Load(Map map)
        {
            //CurrentEquiptments cannot be loaded

            CurrentConsumables = map.StoredConsumables.FilterByGlobalID(CurrentConsumableGuids);
            BoundAttacks = map.StoredAttacks.FilterByGlobalID(BoundAttackGuids);
            PersonalBuffImmunities = PersonalBuffImmunityStorage.Load(map.StoredBuffs);
            PersonalPermanentBuffs = PersonalPermanentBuffStorage.Load(map.StoredBuffs);
        }

        public override void Unload(bool force)
        {
            //CurrentEquiptmentGuids = CurrentEquiptments.ToGlobalIDArray();
            CurrentConsumableGuids = CurrentConsumables.ToGlobalIDList();

            if (force)
            {
                BoundAttackGuids = BoundAttacks.ToGlobalIDList();
                PersonalBuffImmunityStorage.Unload(PersonalBuffImmunities);
                PersonalPermanentBuffStorage.Unload(PersonalPermanentBuffs);
            }
        }

        ///// <summary>
        ///// Consumes an consumable
        ///// </summary>
        ///// <param name="index">The index of the item in PlayerConsumables</param>
        ///// <exception cref="ArgumentOutOfRangeException"></exception>
        //public void Consume(int index)
        //{
        //    Consumable consumable = GameBase.CurrentGame.PlayerConsumables[index];
        //    GameBase.CurrentGame.PlayerConsumables.RemoveAt(index);
        //    CurrentConsumables.Add(consumable);
        //}

        public abstract void Consume(Consumable consumable);

        /// <summary>
        /// Do an attack with the attacker being this character
        /// </summary>
        /// <param name="attack"></param>
        /// <param name="target"></param>
        /// <exception cref="NotEnoughManaException"></exception>
        /// <exception cref="CannotTargetException"></exception>
        public void DoAttack(Attack attack, Character target)
        {
            try
            {
                attack.BeginAttack(this, target);
            }
            catch (NotEnoughManaException e)
            {
                throw e;
            }
            catch (CannotTargetException e)
            {
                throw e;
            }
        }

        //public void Attack(Attack attack, List<Character> targets)
        //{
        //    try
        //    {
        //        attack.BeginAttack(this, targets);
        //    }
        //    catch (NotEnoughManaException e)
        //    {
        //        throw e;
        //    }
        //    catch (CannotTargetException e)
        //    {
        //        throw e;
        //    }
        //}

        public void Charge(Attack attack)
        {
            attack.Charge(this);
        }

        /// <summary>
        /// Tries to continues charge.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public void TryContinueCharge()
        {
            if (!IsCharging)
                throw new ArgumentException("Character must be charging an attack.");

            try
            {
                AttackToCharge.Charge(this);
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        public void RecoverMana()
        {
            AppliedStats[1] += (int)Math.Round(LeveledStats[1] * 0.3);
            GameBase.CurrentGame.CurrentBattleField.NewTurn(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newLocation"></param>
        public void MoveOnBattleField(Point newLocation)
        {
            int distance = BattleField.GetDistance(BattleFieldLocation, newLocation);
            
            if (distance <= MovementPoints)
            {
                BattleFieldLocation = newLocation;
            }
            else
            {

            }            
        }

        /// <summary>
        /// Automatically loads ArmoredImage
        /// </summary>
        public void LoadImage()
        {
            if (StyleOfModel == ModelStyle.Humanoid)
            {
                Bitmap headIcon = null;
                Bitmap torsoIcon = null;
                Bitmap legsIcon = null;

                if (CurrentEquiptments[(int)Equiptment.SlotMode.Head - 1] != null)
                    headIcon = CurrentEquiptments[(int)Equiptment.SlotMode.Head - 1].Icon;
                if (CurrentEquiptments[(int)Equiptment.SlotMode.Torso - 1] != null)
                    torsoIcon = CurrentEquiptments[(int)Equiptment.SlotMode.Torso - 1].Icon;
                if (CurrentEquiptments[(int)Equiptment.SlotMode.Legs - 1] != null)
                    legsIcon = CurrentEquiptments[(int)Equiptment.SlotMode.Legs - 1].Icon;

                LoadImage(
                    headIcon, torsoIcon, legsIcon,
                    CurrentEquiptments[(int)Equiptment.SlotMode.Hand2 - 1]
                    );
            }
            else
            {
                ArmoredImage = WorldImg;
            }
        }

        public bool IsInvunerable()
        {
            foreach (Buff buff in CurrentBuffs)
            {
                if (buff.TypeOfBuff == Buff.BuffType.Invunerable)
                    return true;
            }

            return false;
        }

        public virtual void Die()
        {
            if (GameBase.CurrentGame.CurrentBattleField != null)
            {
                GameBase.CurrentGame.CurrentBattleField.NotifyDeath(this);
            }
        }

        public void EndTurn()
        {
            CheckIfDead();
            TurnsPassed++;
            TurnsSinceLastAttack++;
        }

        public void StartTurn()
        {
            CheckIfDead();
            MovementPoints = CurrentStats[4];
        }

        public void StartRound()
        {
            //Reset TurnTicks
            TurnTicks = 0;

            //Remove all expired consumables and buffs
            CurrentConsumables.RemoveAll(item => item.HasEnded);
            AppliedBuffs.RemoveAll(item => item.HasEnded);
        }
        
        public void EndRound()
        {
            //Get dealt any stats from buffs
            foreach (Buff buff in CurrentBuffs)
                buff.DealStats(this);
        }

        public void CheckIfDead()
        {
            if (IsDead)
                Die();
        }

        public void GetAttacked(Attack attack)
        {
            bool canUseLastHope = CurrentStats[0] != 1;
            List<string> appendedLines = new List<string>();

            if (RNG.GetRandomDouble() < attack.Accuracy)
            {
                attack.DealStats(this);
                attack.DealBuffs(this);

                if (RNG.GetRandomDouble() < attack.CriticalHitChance)
                {
                    attack.DealStats(this);
                    attack.DealBuffs(this);
                    appendedLines.Add("A critical hit!");
                }
            }
            

            foreach (Buff buff in CurrentBuffs)
            {
                if (buff.TypeOfBuff == Buff.BuffType.LastHope && canUseLastHope)
                {
                    appliedStats[0] = (-1 * CurrentStats[0]) + 1;
                    appendedLines.Add($"{Name} was saved by Last Hope");
                    break;
                }
            }

            GameBase.CurrentGame.CurrentBattleField.NotifyAttackHit(attack, this, appendedLines);
        }

        /// <summary>
        /// Gets all multitarget attacks
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="characters"></param>
        /// <returns></returns>
        public List<Attack> GetMultiTargetAttacks<T>(T[] characters) where T : Character
        {
            List<Attack> multiTargetAttacks = new List<Attack>();

            foreach (Attack attack in AllAttacks)
            {
                if (attack.RadiusOfEffect > 0)
                {
                    multiTargetAttacks.Add(attack); 
                }
            }

            return multiTargetAttacks;
        }

        public Attack FindStrongestAttack(Character target, GameBase.Stats stat)
        {
            return FindStrongestAttack(target, stat, AllAttacks, false);
        }

        public Attack FindStrongestAttack(Character target, GameBase.Stats stat, List<Attack> attacks, bool ignoreRange)
        {
            return Attack.FindStrongestAttack(this, target, stat, attacks, ignoreRange);
        }

        public Attack FindStrongestAttack<T>(Character target, T[] bystanders, GameBase.Stats stat, List<Attack> attacks, bool ignoreRange) where T : Character
        {
            return Attack.FindStrongestAttack(this, target, bystanders, stat, attacks, ignoreRange);
        }

        public Attack FindBestHealingAttack(Character target, GameBase.Stats stat)
        {
            return FindBestHealingAttack(target, stat, AllAttacks);
        }

        public Attack FindBestHealingAttack(Character target, GameBase.Stats stat, List<Attack> attacks)
        {
            return Attack.FindBestHealingAttack(this, target, stat, attacks);
        }

        /// <summary>
        /// Gives the distance between two characters by the crow flies.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public double DirectDistanceFrom(Character character)
        {
            return MiscMethods.DistanceFormula(BattleFieldLocation, character.BattleFieldLocation);
        }

        /// <summary>
        /// Returns true if character's percent stats are above the given percentStatsThreshold
        /// </summary>
        /// <param name="percentStatsThreshold"></param>
        /// <returns></returns>
        public bool IsWellOff(double[] percentStatsThreshold)
        {
            double[] percentStats = PercentStats;

            for (int i = 0; i < GameBase.NumStats; i++)
            {
                try
                {
                    if (percentStats[i] < percentStatsThreshold[i])
                    {
                        return false;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    
                }
            }

            return true;
        }
    }
}
