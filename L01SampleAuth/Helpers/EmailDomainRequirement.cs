using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace L01SampleAuth.Helpers
{
    public class EmailDomainRequirement : IAuthorizationRequirement
    {
        public EmailDomainRequirement(string emailDomain)
        {
            EmailDomain = emailDomain;
        }

        public string EmailDomain { get; private set; }
    }

    public class EmailDomainHandler : AuthorizationHandler<EmailDomainRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmailDomainRequirement requirement)
        {
           if(!context.User.HasClaim(c=> c.Type == ClaimTypes.Email))
            {
                return Task.CompletedTask;
            }

            var emailAddress = context.User.FindFirst(c=> c.Type == ClaimTypes.Email).Value;
            if (emailAddress.EndsWith(requirement.EmailDomain))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;

        }
    }

}
