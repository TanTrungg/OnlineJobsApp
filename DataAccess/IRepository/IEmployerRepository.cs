using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IEmployerRepository
    {
        // Repo of Employer
        Company GetCompanyByEmailAndPassword(string email, string password);

        Task<Company> GetCompanyByEmail(string email);


        Task<Company> GetCompanyById(int id);

        Task AddCompany(Company company);

        Task UpdateCompany(Company company);

        // Repo of Post
        Task<List<Post>> GetPostListByComId(int id);

        Task<Post> GetPostById(int? id);

        Task CreatePost(Post post);

        Task UpdatePost(Post post);

        Task DeletePost(Post post);

        Task OpenPost(Post post);

        Task ClosePost(Post post);

        OnlineJobSeekingManagementContext GetMajorCt();

        // Repo of Candidate
        Task<List<Candidate>> GetCandidateListByPostId(int id);

        Task<Candidate> GetCandidateById(int id);

        Task AcceptCandidate(Candidate c);

        Task DenyCandidate(Candidate c);

        //Tấn Trung
        void addCandidate(Candidate candidate);
        List<Candidate> GetCandidateByUserId(int id);
        Boolean CheckUserApply(int postID, int userID);
        Boolean CheckCandidate(int postID, int userID);
    }
}
