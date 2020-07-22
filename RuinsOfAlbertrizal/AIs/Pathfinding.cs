using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RuinsOfAlbertrizal.Characters;
using RuinsOfAlbertrizal.Environment;
using RuinsOfAlbertrizal.Exceptions;
using Point = System.Drawing.Point;

namespace RuinsOfAlbertrizal.AIs
{
    public static class Pathfinding
    {
        /// <summary>
        /// Simple path finding algorithm.
        /// </summary>
        /// <param name="attacker">The enemy doing the attack</param>
        /// <param name="target">The character the enemy is navigating to</param>
        /// <param name="range">The range of the attack the enemy is going to use</param>
        /// <param name="keepDistance">If true, then attacker will move away from target until it can barely hit the target.</param>
        /// <exception cref="DidNotMoveException"></exception>
        public static void PathFind_Direct(Enemy attacker, Character target, int range, bool keepDistance = false)
        {
            int spacesLeft = attacker.CurrentStats[4];
            Point attackerPoint = attacker.BattleFieldLocation;
            Point oldLocation = attackerPoint;
            Point targetPoint = target.BattleFieldLocation;

            //Gets in range
            while (MiscMethods.DistanceFormula(attackerPoint, targetPoint) > range && spacesLeft > 0)
            {
                if (attackerPoint.X > targetPoint.X)
                {
                    //Attacker is right of target. Move left.
                    attackerPoint.X--;
                }
                else if (attackerPoint.X < targetPoint.Y)
                {
                    //Attacker is left of target. Move right.
                    attackerPoint.X++;
                }
                else if (attackerPoint.Y > targetPoint.Y)
                {
                    //Attacker is above of target. Move down.
                    attackerPoint.Y--;
                }
                else
                {
                    //Attacker is below of target. Move up.
                    attackerPoint.Y++;
                }
                spacesLeft--;
            }

            //Backs off
            if (keepDistance)
            {
                while (MiscMethods.DistanceFormula(attackerPoint, targetPoint) < range - 1 && spacesLeft > 0)
                {
                    if (attackerPoint.X > targetPoint.X)
                    {
                        //Attacker is right of target. Move right.
                        attackerPoint.X++;
                    }
                    else if (attackerPoint.X < targetPoint.Y)
                    {
                        //Attacker is left of target. Move left.
                        attackerPoint.X--;
                    }
                    else if (attackerPoint.Y > targetPoint.Y)
                    {
                        //Attacker is above of target. Move up.
                        attackerPoint.Y++;
                    }
                    else
                    {
                        //Attacker is below of target. Move down.
                        attackerPoint.Y--;
                    }
                    spacesLeft--;
                }
            }

            if (oldLocation == attackerPoint)
            {
                throw new DidNotMoveException($"Enemy {attacker.Name} did not move. Try to attack?");
            }
            else
            {
                attacker.BattleFieldLocation = attackerPoint;
                GameBase.CurrentGame.CurrentBattleField.FinalizeMovement(attacker, oldLocation);
            }
        }


        //public static void PathFind_AvoidPlayers(Enemy attacker, Character target, List<Player> players, int range)
        //{
        //    int spacesLeft = attacker.CurrentStats[4];
        //    Point attackerPoint = attacker.BattleFieldLocation;
        //    Point oldLocation = attackerPoint;
        //    Point targetPoint = target.BattleFieldLocation;

        //    //Divide BattleField into two parts based on number of players in either side of the field.
        //    //The attacker would want to move in the direction with the least amount of players while still being within range of attack
        //    List<Player> playersOnLeft = new List<Player>(), playersOnRight = new List<Player>();

        //    foreach (Player player in players)
        //    {
        //        if (targetPoint.X > player.BattleFieldLocation.X)
        //        {
        //            playersOnLeft.Add(player);
        //        }
        //        else if (targetPoint.X < player.BattleFieldLocation.X)
        //        {
        //            playersOnRight.Add(player);
        //        }
        //    }

        //    Gets in range the first time
        //    while (MiscMethods.DistanceFormula(attackerPoint, targetPoint) > range && spacesLeft > 1)
        //    {
        //        if (attackerPoint.X > targetPoint.X)
        //        {
        //            Attacker is right of target.Move left.
        //            attackerPoint.X--;
        //        }
        //        else if (attackerPoint.X < targetPoint.Y)
        //        {
        //            Attacker is left of target.Move right.
        //            attackerPoint.X++;
        //        }
        //        else if (attackerPoint.Y > targetPoint.Y)
        //        {
        //            Attacker is above of target.Move down.
        //            attackerPoint.Y--;
        //        }
        //        else
        //        {
        //            Attacker is below of target.Move up.
        //            attackerPoint.Y++;
        //        }
        //        spacesLeft--;
        //    }

        //    Back up until the attacker is just out of range from the target
        //    while (MiscMethods.DistanceFormula(attackerPoint, targetPoint) < range && spacesLeft > 1)
        //    {
        //        if (attackerPoint.X > targetPoint.X)
        //        {
        //            Attacker is right of target.Move right.
        //            attackerPoint.X++;
        //        }
        //        else if (attackerPoint.X < targetPoint.Y)
        //        {
        //            Attacker is left of target.Move left.
        //            attackerPoint.X--;
        //        }
        //        else if (attackerPoint.Y > targetPoint.Y)
        //        {
        //            Attacker is above of target.Move up.
        //            attackerPoint.Y++;
        //        }
        //        else
        //        {
        //            Attacker is below of target.Move down.
        //            attackerPoint.Y--;
        //        }
        //        spacesLeft--;
        //    }

        //    Get in range again
        //    while (MiscMethods.DistanceFormula(attackerPoint, targetPoint) > range && spacesLeft > 0)
        //    {
        //        if (attackerPoint.X > targetPoint.X)
        //        {
        //            Attacker is right of target.Move left.
        //            attackerPoint.X--;
        //        }
        //        else if (attackerPoint.X < targetPoint.Y)
        //        {
        //            Attacker is left of target.Move right.
        //            attackerPoint.X++;
        //        }
        //        else if (attackerPoint.Y > targetPoint.Y)
        //        {
        //            Attacker is above of target.Move down.
        //            attackerPoint.Y--;
        //        }
        //        else
        //        {
        //            Attacker is below of target.Move up.
        //            attackerPoint.Y++;
        //        }
        //        spacesLeft--;
        //    }

        //    if (oldLocation == attackerPoint)
        //    {
        //        throw new DidNotMoveException($"Enemy {attacker.Name} did not move. Try to attack?");
        //    }
        //    else
        //    {
        //        attacker.BattleFieldLocation = attackerPoint;
        //        GameBase.CurrentGame.CurrentBattleField.FinalizeMovement(attacker, oldLocation);
        //    }
        //}

        public static void PathFind_RunFromPlayers(Enemy attacker, List<Player> players, int spacesLeft)
        {
            Point attackerPoint = attacker.BattleFieldLocation;
            Point oldLocation = attackerPoint;

            //Divide BattleField into two parts based on number of players in either side of the field.
            //The attacker would want to move in the direction with the least amount of players while still being within range of attack
            List<Player> playersOnLeft = new List<Player>(), playersOnRight = new List<Player>();

            foreach (Player player in players)
            {
                if (attacker.BattleFieldLocation.X > player.BattleFieldLocation.X)
                {
                    playersOnLeft.Add(player);
                }
                else if (attacker.BattleFieldLocation.X < player.BattleFieldLocation.X)
                {
                    playersOnRight.Add(player);
                }
            }

            if (playersOnLeft.Count > playersOnRight.Count)
            {
                attackerPoint.X = Math.Max(0, attackerPoint.X - spacesLeft);

                
            }
            else
            {
                attackerPoint.X = Math.Min(BattleField.BattleFieldWidth, attackerPoint.X + spacesLeft);
            }

            spacesLeft -= Math.Abs(oldLocation.X - attackerPoint.X);

            if (spacesLeft > 0)
            {
                Player closestPlayer = players[0];
                double minDistance = double.PositiveInfinity;

                foreach (Player player in players)
                {
                    if (player == null)
                        continue;

                    double distance = MiscMethods.DistanceFormula(attackerPoint, player.BattleFieldLocation);

                    if (minDistance < distance)
                    {
                        closestPlayer = player;
                        minDistance = distance;
                    }
                }

                Point playerPoint = closestPlayer.BattleFieldLocation;

                while (spacesLeft > 0)
                {
                    if (attackerPoint.Y > playerPoint.Y)
                    {
                        //Attacker is above of target. Move up.
                        attackerPoint.Y++;

                        if (attackerPoint.Y == BattleField.BattleFieldHeight - 1)
                            break;
                    }
                    else
                    {
                        //Attacker is below of target. Move down.
                        attackerPoint.Y--;
                    }
                    spacesLeft--;
                }
            }

            if (oldLocation == attackerPoint)
            {
                throw new DidNotMoveException($"Enemy {attacker.Name} did not move. Try to attack?");
            }
            else
            {
                attacker.BattleFieldLocation = attackerPoint;
                GameBase.CurrentGame.CurrentBattleField.FinalizeMovement(attacker, oldLocation);
            }
        }

        public static void PathFind_RunFromPlayers(Enemy attacker, List<Player> players)
        {
            try
            {
                PathFind_RunFromPlayers(attacker, players, attacker.CurrentStats[4]);
            }
            catch (DidNotMoveException)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns a pair of points that are farthest in distance from each other
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Point[] MaxDistanceNaive(Point[] points)
        {
            Point[] selectedPoints = { points[0], points[1] };
            double minDistance = MiscMethods.DistanceFormula(points[0], points[1]);

            for (int i = 0; i < points.Length; i++)
            {
                for (int j = i + 1; j < points.Length; j++)
                {
                    if (MiscMethods.DistanceFormula(points[i], points[j]) < minDistance)
                    {
                        minDistance = MiscMethods.DistanceFormula(points[i], points[j]);
                        selectedPoints[0] = points[i];
                        selectedPoints[1] = points[j];
                    }
                }
            }

            return selectedPoints;
        }
    }
}
