﻿using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Bulletin;

namespace MJ_CAIS.Services.Contracts
{
    public interface IRehabilitationService : IBaseAsyncService<BulletinBaseDTO, BulletinBaseDTO, BulletinGridDTO, BBulletin, string>
    {
        Task ApplyRehabilitation(string bulletinId, string personId);
    }
}
