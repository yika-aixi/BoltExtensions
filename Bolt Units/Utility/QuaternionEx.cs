//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年07月16日-17:58
//Assembly-CSharp

using UnityEngine;

namespace CabinIcarus.BoltExtensions.Utility
{
    public static class QuaternionEx
    {
        /// <summary>
        /// Quaternion To Vector4
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Vector4 ToVect4(this Quaternion self)
        {
            return new Vector4(self.x,self.y,self.z,self.w);
        }
        
        /// <summary>
        /// Vector4 To Quaternion
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Quaternion ToQuaternion(this Vector4 self)
        {
            var qu = Quaternion.identity;
            
            qu.Set(self.x,self.y,self.z,self.w);
            
            return qu;
        }
    }
}