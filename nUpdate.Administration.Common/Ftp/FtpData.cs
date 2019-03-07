﻿using System;
using System.Runtime.Serialization;
using Starksoft.Aspen.Ftps;

namespace nUpdate.Administration.Common.Ftp
{
    // ReSharper disable once InconsistentNaming
    public class FtpData : ITransferData
    {
        private object _secret;

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            Secret = KeyManager.Instance[Identifier];
        }

        /// <summary>
        ///     Gets or sets the specific FTP protocol to use.
        /// </summary>
        public FtpsSecurityProtocol FtpSpecificProtocol { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether passive move should be used, or not.
        /// </summary>
        public bool UsePassiveMode { get; set; }

        /// <summary>
        ///     Gets or sets the host.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        ///     Gets or sets the port.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        ///     Gets or sets the directory.
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        ///     Gets or sets the username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     Gets the identifier for the key database.
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public Uri Identifier => new Uri($"ftp://{Uri.EscapeDataString(Username)}@{Host}:{Port}/");

        /// <summary>
        ///     Gets or sets the secret.
        /// </summary>
        /// <remarks>Holds a password as string.</remarks>
        [Newtonsoft.Json.JsonIgnore]
        public object Secret
        {
            get => _secret;
            set
            {
                _secret = value;
                KeyManager.Instance[Identifier] = value;
            }
        }
    }
}
