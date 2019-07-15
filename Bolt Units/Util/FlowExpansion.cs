//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//2018年08月21日-10:14
//Icarus.UnityGameFramework.Bolt

using System;
using Ludiq.Bolt;
using UnityEngine;

namespace CabinIcarus.BoltExtensions.Util
{
    public static class FlowExpansion
    {

        public static bool EnterTryControl(this Flow flow, ControlOutput control)
        {
            try
            {
                flow.EnterControl(control);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex.Message);
            }

            return false;
        }

        public static void EnterControl(this Flow flow, ControlOutput control)
        {
            if (flow != null)
            {
                if (control != null)
                {
                    flow.Invoke(control);
                }
                else
                {
                    throw new Exception("Control is null, Enter Control Failure!");
                }
            }
            else
            {
                throw new Exception("Flow is null, Enter Control Failure!");
            }
        }

        public static void EnterControlAndDisplay(this Flow flow, ControlOutput control)
        {
            flow.EnterControl(control);
            flow.Dispose();
        }

        public static bool EnterTryControlAndDispose(this Flow flow, ControlOutput control)
        {
            var result = flow.EnterTryControl(control);

            if (result)
            {
                flow.Dispose();
            }

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="flow"></param>
        /// <exception cref="Exception">Flow  或 GraphStack 是空的</exception>
        /// <returns></returns>
        public static Flow GetNewFlow(this Flow flow)
        {
            if (flow == null)
            {
                throw new Exception("Flow is null, Get New Flow Failure!");
            }

            if (flow.stack == null)
            {
                throw new Exception("Flow of GraphStack is null, Get New Flow Failure!");
            }

            return flow.GetNewFlow();
        }
    }
}