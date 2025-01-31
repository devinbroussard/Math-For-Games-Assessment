﻿using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace GameAbilitiesDemo
{
    class AABBCollider : Collider
    {
        private float _width;
        private float _height;
        private float _length;

        /// <summary>
        /// The size of the collider on the x axis
        /// </summary>
        public float Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// The size of the collider on the y axis
        /// </summary>
        public float Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public float Length
        {
            get { return _length; }
            set { _length = value; }
        }


        /// <summary>
        /// The farthest left x position of this collider
        /// </summary>
        public float Left
        {
            get { return Owner.LocalPosition.X - Width / 2; }
        }

        /// <summary>
        /// The farthest right x position of this collider
        /// </summary>
        public float Right
        {
            get { return Owner.LocalPosition.X + Width / 2; }
        }

        /// <summary>
        /// The highest y position of this collider
        /// </summary>
        public float Top
        {
            get { return Owner.LocalPosition.Y - Height / 2; }
        }

        /// <summary>
        /// The lowest y position of this collider
        /// </summary>
        public float Bottom
        {
            get { return Owner.LocalPosition.Y + Height / 2; }
        }

        /// <summary>
        /// The highest z position of this collider
        /// </summary>
        public float Front
        {
            get { return Owner.LocalPosition.Z + Width / 2; }
        }

        /// <summary>
        /// The lowest z position of this collider
        /// </summary>
        public float Back
        {
            get { return Owner.LocalPosition.Z - Width / 2; }
        }

        public AABBCollider(float width, float height, float length, Actor owner)
            : base(owner, ColliderType.AABB)
        {
            _width = width;
            _height = height;
            _length = length;
            owner.Collider = this;
        }

        /// <summary>
        /// Checks for collision with an AABB collider
        /// </summary>
        /// <param name="other">The other AABB collider</param>
        /// <returns>True if a collision occured</returns>
        public override bool CheckCollisionAABB(AABBCollider other)
        {
            //If the other collider is THIS collider...
            if (other.Owner == Owner)
                //...return false
                return false;

            //Return true if there is an overlap between colliders
            return other.Left <= Right &&
                other.Top <= Bottom && 
                Left <= other.Right && 
                Top <= other.Bottom &&
                Front >= other.Back &&
                other.Back <= Front;
        }

        /// <summary>
        /// Checks for collision with a circle collider
        /// </summary>
        /// <param name="other">The circle collider that collision will be checked with.</param>
        /// <returns></returns>
        public override bool CheckCollisionCircle(CircleCollider other)
        {
            return other.CheckCollisionAABB(this);
        }
    }
}
