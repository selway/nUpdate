﻿// Author: Dominic Beger (Trade/ProgTrade)

namespace nUpdate.Administration.Common.Logging
{
    internal class PackageActionLogger
    {
        private readonly UpdateProject _project;

        internal PackageActionLogger(UpdateProject project)
        {
            _project = project;
        }

        public UpdateProject Clear()
        {
            _project.LogData.Clear();
            _project.Save();

            return _project;
        }

        public UpdateProject AppendEntry(PackageActionType type, string packageName)
        {
            var logData = new PackageActionLogData(type, packageName);
            _project.LogData.Add(logData);
            _project.Save();

            return _project;
        }
    }
}