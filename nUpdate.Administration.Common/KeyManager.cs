﻿// Author: Dominic Beger (Trade/ProgTrade) 2017

using System;
using System.Collections.Generic;
using System.IO;

namespace nUpdate.Administration.Common
{
    public sealed class KeyManager : Singleton<KeyManager>
    {
        private bool _initialized;
        private Dictionary<Uri, object> _secrets = new Dictionary<Uri, object>();

        private void EnsureObjectState()
        {
            if (!_initialized)
                throw new InvalidOperationException();
        }
        
        public object this[Uri identifier]
        {
            get
            {
                EnsureObjectState();

                if (identifier == null)
                    throw new ArgumentNullException(nameof(identifier));
                return _secrets[identifier];
            }
            set
            {
                EnsureObjectState();

                var secret = value;
                if (identifier == null)
                    throw new ArgumentNullException(nameof(identifier));
                if (secret == null)
                    throw new ArgumentNullException(nameof(secret));
                if (!secret.GetType().IsSerializable)
                    throw new ArgumentException("Must be serializable.", nameof(secret));

                if (_secrets.ContainsKey(identifier))
                    _secrets[identifier] = secret;
                else
                    _secrets.Add(identifier, secret);
                Save();
            }
        }

        public void Initialize(string password)
        {
            if (!File.Exists(PathProvider.KeyDatabaseFilePath))
                throw new InvalidOperationException("The key database could not be found.");

            byte[] data = File.ReadAllBytes(PathProvider.KeyDatabaseFilePath);
            if ((bool)SettingsManager.Instance["UseEncryptedKeyDatabase"])
                data = AesCryptoProvider.Decrypt(data, password);
            _secrets = BinarySerializer.DeserializeType<Dictionary<Uri, object>>(data);
            _initialized = true;
        }

        public void Save()
        {
            byte[] data = BinarySerializer.Serialize(_secrets);
            if ((bool)SettingsManager.Instance["UseEncryptedKeyDatabase"])
                data = AesCryptoProvider.Encrypt(data, GlobalSession.MasterPassword);
            File.WriteAllBytes(PathProvider.KeyDatabaseFilePath, data);
        }
    }
}