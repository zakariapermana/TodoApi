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

        public TodoItemsController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        // localhost:5001/api/todo
        [HttpGet]
        public IEnumerable<TodoItem> Get()
        {
            // get list todo item
            return _dataAccessProvider.GetTodoRecords();
        }

        // localhost:5001/api/todo
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

        // localhost:5001/api/todo/1
        [HttpGet("{id}")]
        public TodoItem Details(string id)
        {
            // get details todo by id
            return _dataAccessProvider.GetTodoSingleRecord(id);
        }

        // localhost:5001/api/todo
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

        // localhost:5001/api/todo/1
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

        //create new route localhost:5001/api/todo/1/mark-done
        [Route("{id}/mark-done")]
        public IActionResult MarkDone(string id)
        {
            var data = _dataAccessProvider.GetTodoSingleRecord(id);
            // check if the record is exist
            if (data == null)
            {
                return NotFound();
            }
            // then mark as 'Done'
            _dataAccessProvider.MarkDoneTodoRecord(id);
            return Ok();
        }

        //create new route localhost:5001/api/todo/1/percent
        [Route("{id}/percent")]
        public IActionResult PercentComplete(string id, [FromBody] TodoItem todoItem)
        {
            var data = _dataAccessProvider.GetTodoSingleRecord(id);
            // check if the record is exist
            if (data == null)
            {
                return NotFound();
            }
            // then set percent
            data.complete = todoItem.complete;
            _dataAccessProvider.SetPercentTodoRecord(data);
            return Ok();
        }

        //create new route localhost:5001/api/todo/incoming
        [Route("/api/todo/incoming")]
        public List<TodoItem> Incoming()
        {
            return _dataAccessProvider.GetIncomingTodoRecord();
        }
    }
}
