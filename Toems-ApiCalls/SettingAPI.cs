﻿using System.Collections.Generic;
using RestSharp;
using Toems_Common.Dto;
using Toems_Common.Entity;

namespace Toems_ApiCalls
{
    public class SettingAPI : BaseAPI<EntitySetting>
    {
        private readonly ApiRequest _apiRequest;

        public SettingAPI(string resource) : base(resource)
        {
            _apiRequest = new ApiRequest();
        }


        public bool CheckExpiredToken()
        {
            Request.Method = Method.GET;
            Request.Resource = string.Format("{0}/CheckExpiredToken/", Resource);
            var result = _apiRequest.ExecuteExpired<DtoApiBoolResponse>(Request);
            if (result == null) return false;
            return result;
        }

        public string GetSharedLibraryVersion()
        {
            Request.Method = Method.GET;
            Request.Resource = string.Format("{0}/GetSharedLibraryVersion/", Resource);
            var result = _apiRequest.Execute<DtoApiStringResponse>(Request);
            if (result != null)
                return result.Value;
            else
                return "";
        }

        public string GetApplicationVersion()
        {
            Request.Method = Method.GET;
            Request.Resource = string.Format("{0}/GetApplicationVersion/", Resource);
            var result = _apiRequest.Execute<DtoApiStringResponse>(Request);
            if (result != null)
                return result.Value;
            else
                return "";
        }



        public EntitySetting GetSetting(string name)
        {
            Request.Method = Method.GET;
            Request.Resource = string.Format("{0}/GetSetting/", Resource);
            Request.AddParameter("name", name);
            return _apiRequest.Execute<EntitySetting>(Request);
        }

        public bool SendEmailTest()
        {
            Request.Method = Method.GET;
            Request.Resource = string.Format("{0}/SendEmailTest/", Resource);
            var response = _apiRequest.Execute<DtoApiBoolResponse>(Request);
            return response != null && response.Value;
        }

        public bool IsStorageRemote()
        {
            Request.Method = Method.GET;
            Request.Resource = string.Format("{0}/IsStorageRemote/", Resource);
            var response = _apiRequest.Execute<DtoApiBoolResponse>(Request);
            return response != null && response.Value;
        }

        public bool TestADBind()
        {
            Request.Method = Method.GET;
            Request.Resource = string.Format("{0}/TestADBind/", Resource);
            var response = _apiRequest.Execute<DtoApiBoolResponse>(Request);
            return response != null && response.Value;
        }

        public bool UpdateSettings(List<EntitySetting> listSettings)
        {
            Request.Method = Method.POST;
            Request.Resource = string.Format("{0}/UpdateSettings/", Resource);
            Request.AddJsonBody(listSettings);
            var response = _apiRequest.Execute<DtoApiBoolResponse>(Request);
            return response != null && response.Value;
        }

        public IEnumerable<EntityCertificate> GetCAInt()
        {
            Request.Method = Method.GET;
            Request.Resource = string.Format("{0}/GetCAInt/", Resource);
            return _apiRequest.Execute<List<EntityCertificate>>(Request);
        }

        public bool GenerateCAInt()
        {
            Request.Method = Method.GET;
            Request.Resource = string.Format("{0}/GenerateCAInt/", Resource);
            var response = _apiRequest.Execute<DtoApiBoolResponse>(Request);
            return response != null && response.Value;
        }

        public byte[] ExportCert(int id)
        {
            Request.Method = Method.POST;
            Request.Resource = string.Format("{0}/ExportCert/{1}", Resource,id);
            return _apiRequest.ExecuteRaw(Request);
        }

        public string GetClientInstallArgs()
        {
            Request.Method = Method.GET;
            Request.Resource = string.Format("{0}/GetClientInstallArgs/", Resource);
            var response = _apiRequest.Execute<DtoApiStringResponse>(Request);
            if (response != null)
            {
                if (response.Value != null)
                    return response.Value;
                else
                {
                    return string.Empty;
                }
            }
            return string.Empty;
        }

      
    }
}