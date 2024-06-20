using ModelLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class LableRL:ILableRL
    {
        private readonly FundooNotesContext _context;
        public LableRL(FundooNotesContext context)
        {
            _context = context;
        }
        public LabelEntity CreateLable(int userId, CreateLabelRequestModel labelModel)
        {
            // unique label for particuler user
           var getlabel = _context.Labels.FirstOrDefault(x => x.UserId == userId && x.LName==labelModel.LName);
           if(getlabel != null)
            {
                return null;
            }
            LabelEntity label = new LabelEntity();
            label.UserId = userId;
            label.LName = labelModel.LName;

            _context.Labels.Add(label);
           var status =  _context.SaveChanges();
            if (status > 0)
            {
                return label;

            }
            else { return null; }
            
        }

        public LabelEntity GetLablById(int userId, int labelID)
        {
            var getLabel = _context.Labels.FirstOrDefault(x=>x.UserId==userId && x.LabelId==labelID);

            return getLabel;
        
        }

        public List<GetAllLabelResponseModel> getAllLabelID(int userId)
        {
            var data = _context.Labels.Where(x=> x.UserId == userId).ToList();
            List<GetAllLabelResponseModel> alllabels = new List<GetAllLabelResponseModel>();
            foreach (var label in data)
            {
                GetAllLabelResponseModel labelData = new GetAllLabelResponseModel();
                labelData.lName = label.LName;
                labelData.userId = label.LabelId;
                labelData.labelId = label.LabelId;
                alllabels.Add(labelData);

            }
            return alllabels;

        }


        public LabelEntity UpdateLabelById(int userId, UpdateLabelRequestModel labelModel)
        {
            var getLabel = _context.Labels.FirstOrDefault(x => x.UserId == userId && x.LabelId == labelModel.labelID);                      
            getLabel.LName = labelModel.labelName;
            _context.Update(getLabel);
            var status =  _context.SaveChanges();           
            return getLabel;
        }

        public bool DeleteLabelById(int userId, int labelId)
        {
            var getLabel = _context.Labels.FirstOrDefault(x => x.UserId==userId && x.LabelId==labelId);
            var getNoteLabel = _context.NoteLable.FirstOrDefault(x =>  x.LabelID==labelId);

            if (getLabel != null)
            {
                var removeLabel=_context.Labels.Remove(getLabel);
                if(getNoteLabel != null)
                {
                    var removeNoteLabel = _context.NoteLable.Remove(getNoteLabel);
                }
                _context.SaveChanges();
               return true;
            }
            return false;
           


        }
    }

}
