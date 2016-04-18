﻿using Microsoft.Azure.Management.RecoveryServices.Backup;
using Microsoft.Azure.Management.RecoveryServices.Backup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RecoveryServices.Tests.Helpers
{
    public class ProtectedItemTestHelper
    {
        RecoveryServicesBackupManagementClient Client { get; set; }

        public ProtectedItemTestHelper(RecoveryServicesBackupManagementClient client)
        {
            Client = client;
        }

        public BaseRecoveryServicesJobResponse AddOrUpdateProtectedItem(string fabricName,
            string containerName, string protectedItemName, ProtectedItemCreateOrUpdateRequest request)
        {
            string rsVaultRgName = CommonTestHelper.GetSetting(TestConstants.RsVaultRgName);
            string rsVaultName = CommonTestHelper.GetSetting(TestConstants.RsVaultName);
            var customHeader = CommonTestHelper.GetCustomRequestHeaders();

            BaseRecoveryServicesJobResponse response = Client.ProtectedItem.CreateOrUpdateProtectedItem(rsVaultRgName, rsVaultName,
                fabricName, containerName, protectedItemName, request, customHeader);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
            Assert.NotNull(response.Location);
            Assert.NotNull(response.AzureAsyncOperation);
            Assert.NotNull(response.RetryAfter);

            var operationResponse = Client.ProtectedItem.GetProtectedItemOperationResultByURLAsync(response.Location, customHeader);
            while(operationResponse.Result.StatusCode == HttpStatusCode.Accepted)
            {
                System.Threading.Thread.Sleep(5 * 1000);
                operationResponse = Client.ProtectedItem.GetProtectedItemOperationResultByURLAsync(response.Location, customHeader);
            }

            Assert.Equal(HttpStatusCode.OK, operationResponse.Result.StatusCode);
            Assert.NotNull(operationResponse.Result.Item);

            var operationStatusResponse = Client.ProtectedItem.GetOperationStatusByURLAsync(response.AzureAsyncOperation, CommonTestHelper.GetCustomRequestHeaders());
            var operationJobResponse = (OperationStatusJobExtendedInfo)operationStatusResponse.Result.OperationStatus.Properties;

            Assert.Equal(HttpStatusCode.OK, operationStatusResponse.Result.StatusCode);

            Assert.NotNull(operationJobResponse.JobId);
            Assert.Equal(OperationStatusValues.Succeeded, operationStatusResponse.Result.OperationStatus.Status);
            
            return response;
        }

        public BaseRecoveryServicesJobResponse DeleteProtectedItem(string fabricName,
           string containerName, string protectedItemName)
        {
            string rsVaultRgName = CommonTestHelper.GetSetting(TestConstants.RsVaultRgName);
            string rsVaultName = CommonTestHelper.GetSetting(TestConstants.RsVaultName);
            var customHeader = CommonTestHelper.GetCustomRequestHeaders();

            BaseRecoveryServicesJobResponse response = Client.ProtectedItem.DeleteProtectedItem(rsVaultRgName, rsVaultName,
                fabricName, containerName, protectedItemName, customHeader);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
            Assert.NotNull(response.Location);
            Assert.NotNull(response.AzureAsyncOperation);
            Assert.NotNull(response.RetryAfter);

            var operationStatusResponse = Client.ProtectedItem.GetOperationStatusByURLAsync(response.AzureAsyncOperation, customHeader);
            while (operationStatusResponse.Result.OperationStatus.Status == OperationStatusValues.InProgress)
            {
                System.Threading.Thread.Sleep(5 * 1000);
                operationStatusResponse = Client.ProtectedItem.GetOperationStatusByURLAsync(response.AzureAsyncOperation, customHeader);
            }

            operationStatusResponse = Client.ProtectedItem.GetOperationStatusByURLAsync(response.AzureAsyncOperation, CommonTestHelper.GetCustomRequestHeaders());
            var operationJobResponse = (OperationStatusJobExtendedInfo)operationStatusResponse.Result.OperationStatus.Properties;
            Assert.NotNull(operationJobResponse.JobId);
            Assert.Equal(OperationStatusValues.Succeeded, operationStatusResponse.Result.OperationStatus.Status);
            return response;
        }

        public ProtectedItemListResponse ListProtectedItems(ProtectedItemListQueryParam queryParams, PaginationRequest paginationRequest = null)
        {
            string rsVaultRgName = CommonTestHelper.GetSetting(TestConstants.RsVaultRgName);
            string rsVaultName = CommonTestHelper.GetSetting(TestConstants.RsVaultName);

            var response = Client.ProtectedItem.List(rsVaultRgName, rsVaultName, queryParams, paginationRequest, CommonTestHelper.GetCustomRequestHeaders());

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.ItemList);

            return response;
        }
    }
}
