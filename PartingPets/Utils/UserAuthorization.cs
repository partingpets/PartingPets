using PartingPets.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartingPets.Utils
{
    public class UserAuthorization
    {
        //Test Authorization by making sure
        public bool AuthorizeUserByUid(int uid, string jwtId, UsersRepository userRepo)
        {
            var activeUserObj = userRepo.GetUserById(jwtId);
            var userObjFromDb = userRepo.GetUserByDbId(uid);
            //var userFbId = userObjFromDb.FireBaseUid;
            if(userObjFromDb.FireBaseUid != jwtId && activeUserObj.IsAdmin == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //public bool AuthorizeUserByFirebaseId(string fbUid, string jwtId, UsersRepository userRepo)
        //{

        //}


    }

    
}
