using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    // rooute to access the controller
    [Route("api/todo")]
    public class TodoItemsController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        //public TodoItemsController(IDataAccessProvider dataAccessProvider)
        //{
        //    _dataAccessProvider = dataAccessProvider;
        //}

        [HttpGet]
        public IEnumerable<TodoItem> Get()
        {
            // get list todo item
            return _dataAccessProvider.GetTodoRecords();
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                Guid obj = Guid.NewGuid();
                // set unique id
                todoItem.id = obj.ToString();
                // save todo item
                _dataAccessProvider.AddTodoRecord(todoItem);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public TodoItem Details(string id)
        {
            // get details todo by id
            return _dataAccessProvider.GetTodoSingleRecord(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                // update todo record
                _dataAccessProvider.UpdateTodoRecord(todoItem);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(string id)
        {
            var data = _dataAccessProvider.GetTodoSingleRecord(id);
            // check if the record is exist
            if (data == null)
            {
                return NotFound();
            }
            // then delete record 
            _dataAccessProvider.DeleteTodoRecord(id);
            return Ok();
        }
    }
}
