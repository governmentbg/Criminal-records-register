using Microsoft.AspNetCore.Mvc.Rendering;
using MJ_CAIS.Common;
using MJ_CAIS.Common.Resources;
using System.ComponentModel.DataAnnotations;

namespace MJ_CAIS.WebPortal.Public.Models.Feedback
{
    public class FeedbackViewModel
    {
        public FeedbackViewModel()
        {
            this.FeedbackFor = new List<SelectListItem>();
            FeedbackFor.Add(
                new SelectListItem()
                {
                    Text = CommonResources.lblChoose,
                    Value = CommonResources.lblChoose,
                    Selected = true,
                    Disabled = true
                });
            FeedbackFor.Add(
                new SelectListItem()
                {
                    Text = FeedbackResources.lblFeedbackForQuestion,
                    Value = FeedbackResources.lblFeedbackForQuestion
                });
            FeedbackFor.Add(
                new SelectListItem()
                {
                    Text = FeedbackResources.lblFeedbackForProposal,
                    Value = FeedbackResources.lblFeedbackForProposal
                });
            FeedbackFor.Add(
                new SelectListItem()
                {
                    Text = FeedbackResources.lblFeedbackForTechnicalIssue,
                    Value = FeedbackResources.lblFeedbackForTechnicalIssue
                });
        }
        [Display(ResourceType = typeof(FeedbackResources), Name = nameof(FeedbackResources.lblFeedbackFor))]
        [Required(ErrorMessageResourceType = typeof(CommonResources), ErrorMessageResourceName = nameof(CommonResources.MsgRequired))]
        public string? FeedbackForId { get; set; }
        public List<SelectListItem> FeedbackFor { get; set; }

        [Display(ResourceType = typeof(FeedbackResources), Name = nameof(FeedbackResources.lblName))]
        public string? Name { get; set; }

        [EmailAddress(ErrorMessageResourceType = typeof(CommonResources), ErrorMessageResourceName = nameof(CommonResources.MsgEmailInvalid))]
        [Display(ResourceType = typeof(FeedbackResources), Name = nameof(FeedbackResources.lblEmail))]
        [Required(ErrorMessageResourceType = typeof(CommonResources), ErrorMessageResourceName = nameof(CommonResources.MsgRequired))]
        public string? Email { get; set; }

        [Display(ResourceType = typeof(FeedbackResources), Name = nameof(FeedbackResources.lblDescription))]
        [Required(ErrorMessageResourceType = typeof(CommonResources), ErrorMessageResourceName = nameof(CommonResources.MsgRequired))]
        public string? Description { get; set; }


    }
}
