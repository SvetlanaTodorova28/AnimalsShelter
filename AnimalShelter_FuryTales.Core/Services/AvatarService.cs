using AnimalShelter_FuryTales.Constants;

namespace AnimalShelter_FuryTales.Core.Services;

public class AvatarService:IAvatarService{
    public string GetAvatarUrl(string gender, string username){

       
        string baseUrl = "https://api.dicebear.com/5.x/avataaars/svg?";
        string feminineParam = "mouth=default&facialHairProbability=0&top%5B%5D=bigHair,bob,bun,curvy,longButNotTooLong,shaggy,shaggyMullet,shavedSides,straightAndStrand,straight01,straight02&seed=";
        string masculineParam =
            "mouth=default&top%5B%5D=dreads01,dreads02,frizzle,shortCurly,shortFlat,shortRound,shortWaved,sides,theCaesar,theCaesarAndSidePart&seed=";
        if (gender.ToLower() == "female"){
            return $"{baseUrl}{feminineParam}{username}";
                
        }
        if (gender.ToLower() == "male"){
            return $"{baseUrl}{masculineParam}{username}";
            
        }
        //https://api.dicebear.com/5.x/avataaars/svg?mouth=default&top%5B%5D=dreads01,dreads02,frizzle,shortCurly,shortFlat,shortRound,shortWaved,sides,theCaesar,theCaesarAndSidePart&seed=petyr"
            return GlobalConstants.DefaultAvatar;
            
    }
}