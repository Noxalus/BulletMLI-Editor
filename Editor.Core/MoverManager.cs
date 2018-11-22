using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using BulletML;

namespace Editor_Core
{
    public class MoverManager : IBulletManager
    {
        public readonly List<Mover> Movers = new List<Mover>();
        private readonly PositionDelegate _getPlayerPosition;
        public List<Texture2D> BulletTextures;

        private static readonly Random Random = new Random(DateTime.Now.Millisecond);

        public MoverManager(PositionDelegate playerDelegate)
        {
            _getPlayerPosition = playerDelegate;

            BulletML.Configuration.RandomNextFloat = RandomNextFloat;
            BulletML.Configuration.RandomNextInt = RandomNextInt;
            BulletML.Configuration.YUpAxis = true;
        }

        public float RandomNextFloat()
        {
            return (float)Random.NextDouble();
        }

        public int RandomNextInt(int min, int max)
        {
            return Random.Next(min, max);
        }

        public BulletML.Vector2 PlayerPosition(IBullet targettedBullet)
        {
            return _getPlayerPosition();
        }

        public IBullet CreateBullet(bool topBullet = false)
        {
            var mover = new Mover(this);
            mover.Init();

            Movers.Add(mover);

            return mover;
        }

        public void RemoveBullet(IBullet deadBullet)
        {
            var mover = deadBullet as Mover;

            if (mover != null)
                mover.Used = false;
        }

        public void Update(float dt)
        {
            for (int i = 0; i < Movers.Count; i++)
                Movers[i].Update(dt);

            FreeMovers();
        }

        private void FreeMovers()
        {
            for (int i = 0; i < Movers.Count; i++)
            {
                if (!Movers[i].Used)
                {
                    Movers.Remove(Movers[i]);
                    i--;
                }
            }
        }

        public void Clear()
        {
            Movers.Clear();
        }
    }
}