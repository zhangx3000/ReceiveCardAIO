﻿using System;

namespace ArcFaceSharp.Model
{
    /// <summary>
    /// 视频检测缓存实体类
    /// </summary>
    public class FaceTrackUnit
    {
        public MRECT Rect { get; set; }
        public IntPtr Feature { get; set; }
        public string message = "";
    }
}
