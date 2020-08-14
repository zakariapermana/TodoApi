using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Services
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly PostgreSqlContext _context;

        public DataAccessProvider(PostgreSqlContext context)
        {
            _context = context;
        }

        void IDataAccessProvider.AddTodoRecord(TodoItem todoItem)
        {
            // add new todo to database
            _context.todoItems.Add(todoItem);
            _context.SaveChanges();
        }

        void IDataAccessProvider.DeleteTodoRecord(string id)
        {
            // delete record of todo
            var entity = _context.todoItems.FirstOrDefault(t => t.id == id);
            _context.todoItems.Remove(entity);
            _context.SaveChanges();
        }

        List<TodoItem> IDataAccessProvider.GetIncomingTodoRecord()
        {
            // using raw query to access the table where status != 'Done' and date >= now()
            // return _context.todoItems.FromSqlRaw("SELECT * FROM public.todo_item").ToList();
            // Console.WriteLine("SELECT * FROM \"todoItems\" where status!=\"Done\"");
            return _context.todoItems.FromSqlRaw("SELECT * FROM \"todoItems\" where status!='Done' and date::date>=now()::date").ToList();
        }

        List<TodoItem> IDataAccessProvider.GetTodoRecords()
        {
            // get list record of todo
            return _context.todoItems.ToList();
        }

        TodoItem IDataAccessProvider.GetTodoSingleRecord(string id)
        {
            // get a todo record by id
            return _context.todoItems.FirstOrDefault(t => t.id == id);
        }

        void IDataAccessProvider.MarkDoneTodoRecord(string id)
        {
            var data = _context.todoItems.FirstOrDefault(t => t.id == id);
            data.status = "Done";
            _context.todoItems.Update(data);
            _context.SaveChanges();
        }

        void IDataAccessProvider.SetPercentTodoRecord(TodoItem todoItem)
        {
            _context.todoItems.Update(todoItem);
            _context.SaveChanges();
        }

        void IDataAccessProvider.UpdateTodoRecord(TodoItem todoItem)
        {
            // update todo record
            _context.todoItems.Update(todoItem);
            _context.SaveChanges();
        }
    }
}
