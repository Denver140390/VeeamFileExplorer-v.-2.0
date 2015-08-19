﻿using System;

namespace VeeamFileExplorer_v._2._0.ViewModels
{
    interface IFileSystemEntityViewModel
    {
        string Name { get; }
        DateTime LastModifiedTime { get; }
        string Extension { get; }
        long Size { get; }
        string FullPath { get; }
    }
}