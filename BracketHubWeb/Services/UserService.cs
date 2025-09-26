using BracketHubShared.CRUD;
using BracketHubShared.Models;

namespace BracketHubWeb.Services
{
    public class UserService
    {
        protected APIClient APIClient { get; set; }
        public UserService(APIClient aPIClient)
        {
            APIClient = aPIClient;
        }

        public MemberModel? CurrentMember { get; private set; }

        public async Task Signin(MemberReadModel model)
        {
            //CurrentMember = await APIClient.MemberSignin(model);
            //if (CurrentMember.IsNotNull() && CurrentMember.Id != model.Id)
            //    CurrentMember = null;
            CurrentMember = new(model.Id ?? 1, "Yaaaa Nooo", "YaNo");
        }
        public async Task Signup(MemberCreateUpdateModel model)
        {
            //CurrentMember = await APIClient.MemberSignup(model);
            CurrentMember = new(model.Id ?? 1, model.Name ?? "TEST", model.Nickname ?? "TEST");
        }
        public void SignOut()
        {
            CurrentMember = null;
        }
    }
}
