using LoginSignUp.Utils.Enums;
using LoginSignUp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginSignUp.Repository.Interface
{
   public interface IUsers
    {

        SignInEnum SignIn(SignInModel model);
        SignUpEnum SignUp(SignUpModel model);
        ForgotPassEnum ForgotPassword(ForgotpasswordModel model);
        bool VerifyAccount(string otp);
    }
}
