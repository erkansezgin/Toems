﻿using System.Collections.Generic;
using RestSharp;
using Toems_Common.Dto;
using Toems_Common.Entity;

namespace Toems_ApiCalls
{

    public class SysprepAnswerFileAPI : BaseAPI<EntitySysprepAnswerfile>
    {
        public SysprepAnswerFileAPI(string resource) : base(resource)
        {

        }

       
    }
}