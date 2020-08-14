using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Services
{
    interface IDataAccessProvider
    {
        void AddTodoRecord(TodoItem todoItem);
        void UpdateTodoRecord(TodoItem todoItem);
        void DeleteTodoRecord(string id);
        void SetPercentTodoRecord(TodoItem todoItem);
        void MarkDoneTodoRecord(string id);
        TodoItem GetTodoSingleRecord(string id);
        TodoItem GetIncomingTodoRecord(string id);
        List<TodoItem> GetTodoRecords();
    }
}
