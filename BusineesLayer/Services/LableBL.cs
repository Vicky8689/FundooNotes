using BusineesLayer.Interface;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusineesLayer.Services
{
    public class LableBL:ILableBL
    {
        private readonly ILableRL _lableRL;
        public LableBL(ILableRL lableRL)
        {
            _lableRL = lableRL;
        }
        public LabelEntity CreateLable(int userId, CreateLabelRequestModel labelModel)
        {
            return _lableRL.CreateLable(userId, labelModel);
        }


        public LabelEntity GetLablById(int userId, int labelID)
        {
            return _lableRL.GetLablById(userId, labelID);
        }

        public List<GetAllLabelResponseModel> getAllLabelID(int userId)
        {
            return _lableRL.getAllLabelID(userId);
        }

        public LabelEntity UpdateLabelById(int userId, UpdateLabelRequestModel labelModel)
        {
            return _lableRL.UpdateLabelById(userId, labelModel);
        }

        public bool DeleteLabelById(int userId, int labelId)
        {
            return _lableRL.DeleteLabelById(userId, labelId);
        }


    }
}
