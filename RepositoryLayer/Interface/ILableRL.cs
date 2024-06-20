using ModelLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILableRL
    {
        public LabelEntity CreateLable(int userId, CreateLabelRequestModel labelModel);

        public LabelEntity GetLablById(int userId, int labelID);
        public List<GetAllLabelResponseModel> getAllLabelID(int userId);

        public LabelEntity UpdateLabelById(int userId, UpdateLabelRequestModel labelModel);
        public bool DeleteLabelById(int userId,int labelId);



    }
}
