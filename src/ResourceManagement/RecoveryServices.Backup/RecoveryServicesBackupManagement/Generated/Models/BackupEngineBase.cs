// 
// Copyright (c) Microsoft and contributors.  All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// 
// See the License for the specific language governing permissions and
// limitations under the License.
// 

// Warning: This code was generated by a tool.
// 
// Changes to this file may cause incorrect behavior and will be lost if the
// code is regenerated.

using System;
using System.Linq;
using Microsoft.Azure.Management.RecoveryServices.Backup.Models;

namespace Microsoft.Azure.Management.RecoveryServices.Backup.Models
{
    /// <summary>
    /// The definition for BackupEngineBase class.
    /// </summary>
    public partial class BackupEngineBase : BackupEngine
    {
        private string _backupEngineId;
        
        /// <summary>
        /// Optional. BackupEngineId of the managed item.
        /// </summary>
        public string BackupEngineId
        {
            get { return this._backupEngineId; }
            set { this._backupEngineId = value; }
        }
        
        private string _backupEngineType;
        
        /// <summary>
        /// Optional. BackupEngineType of the managed item.
        /// </summary>
        public string BackupEngineType
        {
            get { return this._backupEngineType; }
            set { this._backupEngineType = value; }
        }
        
        private string _backupManagementType;
        
        /// <summary>
        /// Optional. BackupManagement Type of the managed item.
        /// </summary>
        public string BackupManagementType
        {
            get { return this._backupManagementType; }
            set { this._backupManagementType = value; }
        }
        
        private bool _canReRegister;
        
        /// <summary>
        /// Optional. CanReRegister the managed item.
        /// </summary>
        public bool CanReRegister
        {
            get { return this._canReRegister; }
            set { this._canReRegister = value; }
        }
        
        private string _friendlyName;
        
        /// <summary>
        /// Optional. Friendly name of the managed item.
        /// </summary>
        public string FriendlyName
        {
            get { return this._friendlyName; }
            set { this._friendlyName = value; }
        }
        
        private string _healthStatus;
        
        /// <summary>
        /// Optional. Health Status of the managed item.
        /// </summary>
        public string HealthStatus
        {
            get { return this._healthStatus; }
            set { this._healthStatus = value; }
        }
        
        private string _registrationStatus;
        
        /// <summary>
        /// Optional. Registration Status of the managed item.
        /// </summary>
        public string RegistrationStatus
        {
            get { return this._registrationStatus; }
            set { this._registrationStatus = value; }
        }
        
        /// <summary>
        /// Initializes a new instance of the BackupEngineBase class.
        /// </summary>
        public BackupEngineBase()
        {
        }
    }
}
