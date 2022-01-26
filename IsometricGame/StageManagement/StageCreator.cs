﻿using System;

namespace GXPEngine.StageManagement
{
    public class StageCreator : GameObject
    {
        public void SetStage(Enum stage)
        {
            switch (stage)
            {
                case Stages.Tutorial:
                    
                    //Load stage
                    StageLoader.LoadStage(Stages.Tutorial);
                    
                    break;
                
                
                case Stages.Stage1:

                    //Load stage
                    StageLoader.LoadStage(Stages.Stage1);
                    
                    break;
                
                
                case Stages.Stage2:

                    //Load stage
                    StageLoader.LoadStage(Stages.Stage2);
                    
                    break;
            }
        }


    }
}