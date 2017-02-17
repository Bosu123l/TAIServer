using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TAI.Services;
using TAI.Utils;
using TAIServer.Entities;
using TAIServer.Entities.DataAccess;
using TAIServer.Services.Interfaces;

namespace TAIServer.Services
{
    public class MembersService : BaseService, IMembersService
    {
        public MembersService(DataContext datacontext) : base(datacontext)
        {
        }

        public Member GetMember(long id)
        {
            var tempMember = base.DataContext.Members.Include(x => x.Tasks).SingleOrDefault(x => x.Id.Equals(id));
            if (tempMember == null)
            {
                throw new StatusCodeException((int)HttpStatusCode.BadRequest, "No match element!");
            }
            return tempMember;
        }

        public bool RegisterMember(Member member)
        {
            var tempMember = base.DataContext.Members.SingleOrDefault(x => x.Login.Equals(member.Login));
            if (tempMember == null)
            {
                try
                {
                    member.Password = Common.Sha256(member.Password);
                    this.AddMember(member);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }

        public Member UpdateMember(Member member)
        {
            Member tempMember;
            try
            {
                tempMember = base.DataContext.Members.Update(member).Entity;
                base.DataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return tempMember;
        }

        public Member RemoveMember(long id)
        {
            Member tempMember;
            try
            {
                tempMember = base.DataContext.Members.Remove(this.GetMember(id)).Entity;
                base.DataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new StatusCodeException((int)HttpStatusCode.BadRequest, "No match element!");
            }

            return tempMember;
        }

        public Member AddMember(Member member)
        {
            Member tempMember;
            try
            {
                tempMember = base.DataContext.Members.Add(member).Entity;
                base.DataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
            return tempMember;
        }

        public IEnumerable<Member> GetMembers()
        {
            var members = base.DataContext.Members.ToList();
            if (members == null || members.Count == 0)
            {
                throw new StatusCodeException((int)HttpStatusCode.BadRequest, "No match element!");
            }

            return members;
        }
    }
}