using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManager.Data;
using StudentManager.Models.ViewModels;


namespace StudentManager.Models
{
    public class DepartmentModels
    {
        StudentManagerContext _context = null ;

        public DepartmentModels()
        {
            _context = new StudentManagerContext();
        }

        public List<DepartmentViewModel> ShowLeaderName()
        {
            var query = from a in _context.Accounts
                        join u in _context.Users on a.UserId equals u.Id
                        where a.RoleId == 2 & a.DeletedAt == null
                        select new DepartmentViewModel
                        {
                            Leader_Id = a.UserId,
                            LeaderName = u.FullName
                        };
            return query.ToList();
        }
        public List<DepartmentViewModel> DepartLeaderName()
        {
            var qurey = from d in _context.Departments
                        join a in _context.Accounts on d.LeaderId equals a.Id
                        join u in _context.Users on a.UserId equals u.Id
                        where a.DeletedAt == null & u.DeletedAt == null & d.DeletedAt == null
                        select new DepartmentViewModel
                        {
                            Name = d.Name,
                            DateBeginning = d.DateBeginning,
                            Status = d.Status,
                            Logo = d.Logo,
                            Leader_Id = d.LeaderId,
                            LeaderName = u.FullName
                        };
            return qurey.ToList();
        }
    }
}
