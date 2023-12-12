using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class EmployerRepository : IEmployerRepository
    {
        //Repo of Employer
        public Task<Company> GetCompanyByEmail(string email) => EmployerDAO.Instance.GetCompanyByEmail(email);

        public Company GetCompanyByEmailAndPassword(string email, string password) => EmployerDAO.Instance.GetCompanyByEmailAndPassword(email, password);

        public Task<Company> GetCompanyById(int id) => EmployerDAO.Instance.GetCompanyById(id);

        public Task AddCompany(Company company) => EmployerDAO.Instance.AddCompany(company);

        public Task UpdateCompany(Company company) => EmployerDAO.Instance.UpdateCompany(company);

        // Repo of Post
        public Task<List<Post>> GetPostListByComId(int id) => PostManagementDAO.Instance.GetPostListByComId(id);

        public Task<Post> GetPostById(int? id) => PostManagementDAO.Instance.GetPostById(id);    

        public Task CreatePost(Post post) => PostManagementDAO.Instance.CreatePost(post);

        public Task UpdatePost(Post post) => PostManagementDAO.Instance.UpdatePost(post); 

        public Task DeletePost(Post post) => PostManagementDAO.Instance.DeletePost(post);

        public Task ClosePost(Post post) => PostManagementDAO.Instance.CLosePost(post);

        public Task OpenPost(Post post) => PostManagementDAO.Instance.OpenPost(post);

        public OnlineJobSeekingManagementContext GetMajorCt() => PostManagementDAO.Instance.GetMajorCt();

        // Repo of Candidate
        public Task<List<Candidate>> GetCandidateListByPostId(int id) => CandidateManagementDAO.Instance.GetCandidateListByPostId(id);

        public Task<Candidate> GetCandidateById(int id) => CandidateManagementDAO.Instance.GetCandidateById(id);

        public Task AcceptCandidate(Candidate c) => CandidateManagementDAO.Instance.AcceptCandidate(c);

        public Task DenyCandidate(Candidate c) => CandidateManagementDAO.Instance.DenyCandidate(c);

        //Tấn Trung
        public void addCandidate(Candidate candidate) => CandidateManagementDAO.Instance.addCandidate(candidate);

        List<Candidate> IEmployerRepository.GetCandidateByUserId(int id) => CandidateManagementDAO.Instance.GetCandidateByUserId(id);
        public bool CheckUserApply(int postID, int userID) => CandidateManagementDAO.Instance.CheckUserApply(postID, userID);

        public bool CheckCandidate(int postID, int userID) => CandidateManagementDAO.Instance.CheckCandidate(postID, userID);
        
    }
}
