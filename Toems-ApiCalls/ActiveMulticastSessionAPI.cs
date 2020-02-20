﻿using System.Collections.Generic;
using RestSharp;
using Toems_Common.Dto;
using Toems_Common.Entity;

namespace Toems_ApiCalls
{

    public class ActiveMulticastSessionAPI : BaseAPI<EntityActiveMulticastSession>
    {
        private readonly ApiRequest _apiRequest;

        public ActiveMulticastSessionAPI(string resource) : base(resource)
        {
            _apiRequest = new ApiRequest();
        }

        public IEnumerable<EntityComputer> GetComputers(int id)
        {
            Request.Method = Method.GET;
            Request.Resource = string.Format("{0}/GetComputers/{1}", Resource, id);
            var result = _apiRequest.Execute<List<EntityComputer>>(Request);
            if (result == null)
                return new List<EntityComputer>();
            else
                return result;
        }

        public IEnumerable<TaskWithComputer> GetMemberStatus(int id)
        {
            Request.Method = Method.GET;
            Request.Resource = string.Format("{0}/GetMemberStatus/{1}", Resource, id);
            var result = _apiRequest.Execute<List<TaskWithComputer>>(Request);
            if (result == null)
                return new List<TaskWithComputer>();
            else
                return result;
        }

        public IEnumerable<EntityActiveImagingTask> GetProgress(int id)
        {
            Request.Method = Method.GET;
            Request.Resource = string.Format("{0}/GetProgress/{1}", Resource, id);
            var result = _apiRequest.Execute<List<EntityActiveImagingTask>>(Request);
            if (result == null)
                return new List<EntityActiveImagingTask>();
            else
                return result;
        }

        public string StartOnDemandMulticast(int profileId, string clientCount, string sessionName, int comServerId)
        {
            Request.Method = Method.GET;
            Request.Resource = string.Format("{0}/StartOnDemandMulticast/", Resource);
            Request.AddParameter("profileId", profileId);
            Request.AddParameter("clientCount", clientCount);
            Request.AddParameter("comServerId", comServerId);
            Request.AddParameter("sessionName", sessionName);
            var response = _apiRequest.Execute<DtoApiStringResponse>(Request);
            return response != null ? response.Value : string.Empty;
        }

    }
}