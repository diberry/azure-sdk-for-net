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
using Microsoft.Azure.Management.DataLake.AnalyticsCatalog.Models;

namespace Microsoft.Azure.Management.DataLake.AnalyticsCatalog.Models
{
    /// <summary>
    /// A Data Lake Analytics catalog U-SQL view item.
    /// </summary>
    public partial class USqlView : CatalogItem
    {
        private string _databaseName;
        
        /// <summary>
        /// Optional. Gets or sets the name of the database.
        /// </summary>
        public string DatabaseName
        {
            get { return this._databaseName; }
            set { this._databaseName = value; }
        }
        
        private string _definition;
        
        /// <summary>
        /// Optional. Gets or sets the defined query of the view.
        /// </summary>
        public string Definition
        {
            get { return this._definition; }
            set { this._definition = value; }
        }
        
        private string _name;
        
        /// <summary>
        /// Optional. Gets or sets the name of the view.
        /// </summary>
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }
        
        private string _schemaName;
        
        /// <summary>
        /// Optional. Gets or sets the name of the schema associated with this
        /// view and database.
        /// </summary>
        public string SchemaName
        {
            get { return this._schemaName; }
            set { this._schemaName = value; }
        }
        
        /// <summary>
        /// Initializes a new instance of the USqlView class.
        /// </summary>
        public USqlView()
        {
        }
    }
}
