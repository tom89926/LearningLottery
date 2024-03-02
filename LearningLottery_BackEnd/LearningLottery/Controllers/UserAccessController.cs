using System.ComponentModel.DataAnnotations;
using AutoMapper;
using LearningLottery.Core.Services;
using LearningLottery.Models;
using LearningLottery.Services;
using Microsoft.AspNetCore.Mvc;

namespace LearningLottery.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserAccessController : ControllerBase {
    private readonly IUserAccessService _userAccessService;
    private readonly IModelTransformerService _modelTransformerService;
    private readonly IMapper _mapper;

    public UserAccessController(
        IUserAccessService userAccessService,
        IModelTransformerService modelTransformerService, IMapper mapper) {
        _userAccessService = userAccessService;
        _modelTransformerService = modelTransformerService;
        _mapper = mapper;
    }

    [HttpPost("Post/AddUser")]
    public IActionResult AddUser(Models.UserAccess userAccess) {
        var user = _modelTransformerService.OutPutUserAccessServiceModel(userAccess);
        var response = _userAccessService.AddUser(user);
        return response ? Ok("Add success") : BadRequest("Add error");
    }
    
    [HttpDelete("Delete/DeleteUserById")]
    public IActionResult DeleteUserById([Required]int id) {
        var response = _userAccessService.DeleteUserById(id);
        return response ? Ok("Delete success") : BadRequest("Delete error");
    }
    
    [HttpPatch("Patch/UpdateUser")]
    public IActionResult UpdateUser(Models.UserAccess userAccess) {
        if(userAccess.Id == 0)return BadRequest("Update error");
        var user = _modelTransformerService.OutPutUserAccessServiceModel(userAccess);
        var response = _userAccessService.UpdateUser(user);
        return response ? Ok("Update success") : BadRequest("Update error");
    }
    
    [HttpGet("Get/GetAllUser")]
    public IActionResult GetAllUser() {
        var userAccesses = _userAccessService.GetAllUser();
        return Ok(OutputUserAccessList(userAccesses));
    }
    
    [HttpGet("Get/GetUserById")]
    public IActionResult GetUserById([Required]int id) {
        var userAccesses = _userAccessService.GetUserById(id);
        return Ok(OutputUserAccess(userAccesses));
    }
    
    private UserAccess OutputUserAccess(Core.Services.Models.UserAccess userAccess) {
        return _mapper.Map<Core.Services.Models.UserAccess, UserAccess>(userAccess);
    }
    
    private IEnumerable<UserAccess> OutputUserAccessList(IEnumerable<Core.Services.Models.UserAccess> userList) {
        return userList.Select(OutputUserAccess);
    }
}