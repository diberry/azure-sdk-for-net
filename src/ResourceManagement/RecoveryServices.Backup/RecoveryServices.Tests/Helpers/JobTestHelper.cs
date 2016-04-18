﻿using Microsoft.Azure.Management.RecoveryServices.Backup;
using Microsoft.Azure.Management.RecoveryServices.Backup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Runtime.Serialization;
using System.Globalization;

namespace RecoveryServices.Tests.Helpers
{
    public class JobTestHelper
    {
        private string rgName;
        private string rName;

        RecoveryServicesBackupManagementClient Client { get; set; }

        public JobTestHelper(RecoveryServicesBackupManagementClient client)
        {
            this.rgName = CommonTestHelper.GetSetting(TestConstants.RsVaultRgName);
            this.rName = CommonTestHelper.GetSetting(TestConstants.RsVaultName);
            this.Client = client;
        }

        public JobListResponse ListJobs(CommonJobQueryFilters queryFilters, PaginationRequest paginationReq)
        {
            var response = Client.Job.ListAsync(rgName, rName, queryFilters, paginationReq, CommonTestHelper.GetCustomRequestHeaders()).Result;

            Assert.NotNull(response);
            Assert.NotNull(response.ItemList);
            Assert.Equal(response.StatusCode, HttpStatusCode.OK);

            return response;
        }

        public JobResponse GetJob(string jobId)
        {
            var result = Client.Job.GetAsync(rgName, rName, jobId, CommonTestHelper.GetCustomRequestHeaders()).Result;
            Assert.NotNull(result);
            Assert.Equal(result.StatusCode, HttpStatusCode.OK);
            return result;
        }

        public BaseRecoveryServicesJobResponse CancelJob(string jobId)
        {
            var result = Client.Job.CancelJobAsync(rgName, rName, jobId, CommonTestHelper.GetCustomRequestHeaders()).Result;
            Assert.NotNull(result);
            Assert.Equal(result.StatusCode, HttpStatusCode.Accepted);
            return result;
        }

        public JobResponse GetJobOperationStatus(string jobId, string opId)
        {
            var result = Client.Job.GetOperationResultAsync(rgName, rName, jobId, opId, CommonTestHelper.GetCustomRequestHeaders()).Result;
            return result;
        }

        public void ValidateJobResponse(JobBase job, CommonJobQueryFilters filters)
        {
            if (job.GetType() == typeof(AzureIaaSVMJob))
            {
                ValidateIaasVMJob(job as AzureIaaSVMJob, filters);
            }
        }

        public string GetOpId(string fullId)
        {
            Uri fullUri = new Uri(fullId);
            fullId = fullUri.AbsolutePath;
            string[] splitArr = fullId.Split("/".ToCharArray());
            return splitArr[splitArr.Length - 1];
        }

        private void ValidateIaasVMJob(AzureIaaSVMJob job, CommonJobQueryFilters filters)
        {
            Assert.NotNull(job.Status);
            Assert.NotNull(job.BackupManagementType);
            Assert.NotNull(job.Operation);
            Assert.NotNull(job.EntityFriendlyName);

            if (filters != null)
            {
                if (!string.IsNullOrEmpty(filters.StartTime))
                {
                    Assert.True(job.StartTime.CompareTo(DateTime.ParseExact(filters.StartTime, "yyyy-MM-dd hh:mm:ss tt", CultureInfo.InvariantCulture)) >= 0);
                }

                if (!string.IsNullOrEmpty(filters.EndTime))
                {
                    Assert.True(job.StartTime.CompareTo(DateTime.ParseExact(filters.EndTime, "yyyy-MM-dd hh:mm:ss tt", CultureInfo.InvariantCulture)) <= 0);
                }

                if (!string.IsNullOrEmpty(filters.Status))
                {
                    Assert.Equal(filters.Status, job.Status);
                }

                if (!string.IsNullOrEmpty(filters.Operation))
                {
                    Assert.Equal(filters.Operation, job.Operation);
                }
            }

            if (job.ExtendedInfo != null)
            {
                Assert.NotNull(job.ExtendedInfo.PropertyBag);
            }
        }
    }
}
