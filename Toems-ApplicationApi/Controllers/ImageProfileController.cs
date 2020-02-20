﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Toems_ApplicationApi.Controllers.Authorization;
using Toems_Common;
using Toems_Common.Dto;
using Toems_Common.Entity;
using Toems_Common.Enum;
using Toems_Service.Entity;

namespace Toems_ApplicationApi.Controllers
{
    public class ImageProfileController : ApiController
    {
        private readonly ServiceImageProfile _imageProfileService;
        private readonly ServiceAuditLog _auditLogService;
        private readonly int _userId;

        public ImageProfileController()
        {
            _imageProfileService = new ServiceImageProfile();
            _auditLogService = new ServiceAuditLog();
            _userId = Convert.ToInt32(((ClaimsIdentity)User.Identity).Claims.Where(c => c.Type == "user_id")
                .Select(c => c.Value).SingleOrDefault());

        }

        [HttpGet]
        [Authorize]
        public DtoApiBoolResponse Clone(int id)
        {
            _imageProfileService.CloneProfile(id);
            return new DtoApiBoolResponse { Value = true };
        }

        [Authorize]
        public DtoActionResult Delete(int id)
        {
            var image = _imageProfileService.GetImageProfile(id);
            var result = _imageProfileService.Delete(id);
            if (result == null) throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            else
            {
                var auditLog = new EntityAuditLog();
                auditLog.ObjectType = "ImageProfile";
                auditLog.ObjectId = result.Id;
                auditLog.ObjectName = image.Name;
                auditLog.ObjectJson = JsonConvert.SerializeObject(image);
                auditLog.UserId = _userId;
                auditLog.AuditType = EnumAuditEntry.AuditType.Delete;
                _auditLogService.AddAuditLog(auditLog);
                return result;
            }
        }

        [Authorize]
        public EntityImageProfile Get(int id)
        {
            var result = _imageProfileService.GetImageProfile(id);
            if (result == null) throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            return result;
        }


        [Authorize]
        public IEnumerable<EntityImageProfile> Get()
        {
            return _imageProfileService.GetAll();
        }

        [Authorize]
        [HttpPost]
        public IEnumerable<EntityImageProfile> Search(DtoSearchFilter filter)
        {
            return _imageProfileService.Search(filter);
        }

        [Authorize]
        public DtoApiStringResponse GetCount()
        {
            return new DtoApiStringResponse {Value = _imageProfileService.TotalCount()};
        }


        [Authorize]
        public DtoActionResult Post(EntityImageProfile imageProfile)
        {
            var result = _imageProfileService.Add(imageProfile);
            if(result.Success)
            {
                var auditLog = new EntityAuditLog();
                auditLog.ObjectType = "ImageProfile";
                auditLog.ObjectId = result.Id;
                auditLog.ObjectName = imageProfile.Name;
                auditLog.ObjectJson = JsonConvert.SerializeObject(imageProfile);
                auditLog.UserId = _userId;
                auditLog.AuditType = EnumAuditEntry.AuditType.Create;
                _auditLogService.AddAuditLog(auditLog);
            }
            return result;
        }

        [Authorize]
        public DtoActionResult Put(int id, EntityImageProfile imageProfile)
        {
            imageProfile.Id = id;
            var result = _imageProfileService.Update(imageProfile);
            if (result == null) throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            if(result.Success)
            {
                var auditLog = new EntityAuditLog();
                auditLog.ObjectType = "ImageProfile";
                auditLog.ObjectId = result.Id;
                auditLog.ObjectName = imageProfile.Name;
                auditLog.ObjectJson = JsonConvert.SerializeObject(imageProfile);
                auditLog.UserId = _userId;
                auditLog.AuditType = EnumAuditEntry.AuditType.Update;
                _auditLogService.AddAuditLog(auditLog);
            }
            return result;
        }

        [Authorize]
        public DtoApiStringResponse GetMinimumClientSize(int id, int hdNumber)
        {
            return new DtoApiStringResponse
            {
                Value = _imageProfileService.MinimumClientSizeForGridView(id, hdNumber)
            };
        }


        [Authorize]
        public IEnumerable<EntityImageProfileScript> GetScripts(int id)
        {
            return _imageProfileService.GetImageProfileScripts(id);
        }

        [Authorize]
        public IEnumerable<EntityImageProfileSysprepTag> GetSysprep(int id)
        {
            return _imageProfileService.GetImageProfileSysprep(id);
        }

        [Authorize]
        public IEnumerable<EntityImageProfileFileCopy> GetFileCopy(int id)
        {
            return _imageProfileService.GetImageProfileFileCopy(id);
        }

        [Authorize]
        [HttpDelete]
        public DtoApiBoolResponse RemoveProfileFileCopy(int id)
        {
            return new DtoApiBoolResponse { Value = _imageProfileService.DeleteImageProfileFileCopy(id) };
        }

        [Authorize]
        [HttpDelete]
        public DtoApiBoolResponse RemoveProfileScripts(int id)
        {
            return new DtoApiBoolResponse { Value = _imageProfileService.DeleteImageProfileScripts(id) };
        }

        [Authorize]
        [HttpDelete]
        public DtoApiBoolResponse RemoveProfileSysprep(int id)
        {
            return new DtoApiBoolResponse { Value = _imageProfileService.DeleteImageProfileSysprepTags(id) };
        }


    }
}