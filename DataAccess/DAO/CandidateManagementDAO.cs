using BusinessObject.Models;
using DataAccess.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CandidateManagementDAO
    {
        private static CandidateManagementDAO instance = null;
        private static readonly object instanceLock = new object();

        public static CandidateManagementDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CandidateManagementDAO();
                    }
                    return instance;
                }
            }
        }

        public async Task<List<Candidate>> GetCandidateListByPostId(int id)
        {
            List<Candidate> c = new List<Candidate>();
            try
            {
                using var context = new OnlineJobSeekingManagementContext();
                c = await context.Candidates
                    .Where(c => c.IsDeleted == false && c.PostId == id)
                    .OrderByDescending(c => c.CreatedAt)
                    .Include(c => c.User)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return c;
        }

        public async Task<Candidate> GetCandidateById(int id)
        {
            Candidate c = new Candidate();
            try
            {
                using var context = new OnlineJobSeekingManagementContext();
                c = await context.Candidates
                    .Where(c => c.Id == id && c.IsDeleted == false)
                    .Include(c => c.User)
                    .Include(c => c.Post)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return c;
        }

        public async Task AcceptCandidate(Candidate c)
        {
            try
            {
                using var context = new OnlineJobSeekingManagementContext();
                c.UpdatedAt = DateTime.Now;
                c.Status = CommonEnums.CANDIDATE_STATUS.ACCEPT;
                context.Candidates.Update(c);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DenyCandidate(Candidate c)
        {
            try
            {
                using var context = new OnlineJobSeekingManagementContext();
                c.UpdatedAt = DateTime.Now;
                c.Status = CommonEnums.CANDIDATE_STATUS.DENY;
                context.Candidates.Update(c);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        //Tấn Trung
        public void addCandidate(Candidate candidate)
        {
            var _context = new OnlineJobSeekingManagementContext();
            _context.Candidates.Add(candidate);
            _context.SaveChanges();
        }
        public List<Candidate> GetCandidateByUserId(int id)
        {
            var _context = new OnlineJobSeekingManagementContext();
            var resutl = _context.Candidates.Where(c => c.UserId == id && c.IsDeleted == false)
                                .Include(c => c.User)
                                .Include(c => c.Post)
                                    .ThenInclude(p => p.Com)
                                .ToList();
            return resutl;
        }
        public Boolean CheckUserApply(int postID, int userID)
        {
            var _context = new OnlineJobSeekingManagementContext();
            return _context.Candidates.Any(c => c.PostId == postID
                                        && c.UserId == userID
                                        && c.IsDeleted == false
                                        && c.Status == null
                                        );
        }

        public Boolean CheckCandidate(int postID, int userID)
        {
            var _context = new OnlineJobSeekingManagementContext();
            return _context.Candidates.Any(c => c.PostId == postID
                                        && c.UserId == userID
                                        && c.IsDeleted == false
                                        && c.Status == CommonEnums.CANDIDATE_STATUS.ACCEPT
                                        );
        }
    }
}
