﻿using System;
using Starksoft.Aspen.Ftps;

namespace nUpdate.Administration.Common.Ftp
{
    internal class FtpTransferProgressData : TransferProgressEventArgs, ITransferProgressData
    {
        public FtpTransferProgressData(int bytesTransferred, long totalBytesTransferred, long transferSize, int bytesPerSecond, TimeSpan elapsedTime, int percentComplete) : base(bytesTransferred, totalBytesTransferred, transferSize, bytesPerSecond, elapsedTime, percentComplete)
        { }
    }
}