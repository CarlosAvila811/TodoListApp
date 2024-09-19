﻿using SQLite;

namespace TodoListApp.Models
{
    public class Tarea
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; } 
    }
}
