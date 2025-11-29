using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuditLog _auditLog;
        public FavoriteService(IUnitOfWork unitOfWork, IMapper mapper, IAuditLog auditLog)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _auditLog = auditLog;
        }
        public async Task<Result<FavoriteDTO>> CreateAsync(FavoriteDTO FavoriteDTO)
        {

            try
            {
                Favorite promt = _mapper.Map<Favorite>(FavoriteDTO);

                await _unitOfWork.Favorites.AddAsync(promt);
                await _unitOfWork.CompleteAsync();

                FavoriteDTO.Id = promt.Id;

                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Success, RecordId = promt.Id.ToString(), TableName = "Favorites" });

                return Result<FavoriteDTO>.Ok(FavoriteDTO, "Favorite created successfully.");
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Favorites", UserAgent = ex.Message });
                return Result<FavoriteDTO>.Fail("Favorite uncreated.");
            }
        }



        public async Task<Result<bool>> DeleteAsync(int id)
        {
            try
            {
                var Favorite = await _unitOfWork.Favorites.GetByIdAsync(id);

                if (Favorite == null)
                    return Result<bool>.Fail("Favorite not found.");

                _unitOfWork.Favorites.Remove(Favorite);
                await _unitOfWork.CompleteAsync();
                return Result<bool>.Ok(true, "Favorite deleted.");
            }
            catch (Exception ex)
            {

                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Favorites", UserAgent = ex.Message });
                return Result<bool>.Fail("Favorite undeleted.");
            }
        }

        public async Task<Result<IEnumerable<FavoriteDTO>>> GetAllAsync()
        {
            try
            {
                var Favorites = await _unitOfWork.Favorites.GetAllAsync();
                var result = _mapper.Map<IEnumerable<FavoriteDTO>>(Favorites);
                return Result<IEnumerable<FavoriteDTO>>.Ok(result);
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Favorites", UserAgent = ex.Message });
                return Result<IEnumerable<FavoriteDTO>>.Fail("Favorite get exception.");
            }
        }

        public async Task<Result<FavoriteDTO>> GetByIdAsync(int id)
        {
            try
            {
                var Favorite = await _unitOfWork.Favorites.GetByIdAsync(id);

                if (Favorite == null)
                    return Result<FavoriteDTO>.Fail("Favorite not found.");

                FavoriteDTO dto = _mapper.Map<FavoriteDTO>(Favorite);
                return Result<FavoriteDTO>.Ok(dto);
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Favorites", UserAgent = ex.Message });
                return Result<FavoriteDTO>.Fail("Favorite get exception.");
            }

        }

        public async Task<Result<FavoriteDTO>> UpdateAsync(int id, FavoriteDTO FavoriteDTO)
        {
            try
            {
                var Favorite = await _unitOfWork.Favorites.GetByIdAsync(id);

                if (Favorite == null)
                    return Result<FavoriteDTO>.Fail("Favorite not found");

                Favorite promt = _mapper.Map<Favorite>(FavoriteDTO);

                _unitOfWork.Favorites.Update(promt);
                await _unitOfWork.CompleteAsync();
                return Result<FavoriteDTO>.Ok(FavoriteDTO, "Favorite updated successfully");
            }
            catch (Exception ex)
            {
                await _auditLog.CreateAsync(new AuditLog { LogTypeId = LogType.Error, TableName = "Favorites", UserAgent = ex.Message });
                return Result<FavoriteDTO>.Fail("Favorite un updated.");
            }
        }
    }
}
