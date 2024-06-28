using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Scala.StockSimulation.Utilities.Authorization{
    public class AuthorizeMultiplePolicyFilter : IAuthorizationFilter{
        private readonly IAuthorizationService _authorization;
        public string Policies{ get; }
        public bool IsAll{ get; }

        public AuthorizeMultiplePolicyFilter(string policies, bool isAll, IAuthorizationService authorization){
            Policies = policies ?? throw new ArgumentNullException(nameof(policies));
            IsAll = isAll;
            _authorization = authorization;
        }

        public async void OnAuthorization(AuthorizationFilterContext context){
            var policies = Policies.Split(',');
            var successes = 0;

            foreach (var policy in policies){
                var authorized = await _authorization.AuthorizeAsync(context.HttpContext.User, policy.Trim());
                if (authorized.Succeeded){
                    if (!IsAll){
                        return; // Succeed early if any policy succeeds
                    }

                    successes++;
                }
            }

            if (IsAll && successes == policies.Length){
                return; // Succeed only if all policies succeed
            }

            context.Result = new ForbidResult(); // Forbid if not all (or any) policies are met
        }
    }
}
