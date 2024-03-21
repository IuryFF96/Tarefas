using Dapper;
using System;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Tarefas.DTO;
using System.Collections.Generic;

namespace Tarefas.DAO
{
    public class DataBaseBootstrap : BaseDAO, IDataBaseBootstrap
    {
       public void Setup()
       {
            if(!File.Exists(DataSourceFile))
            {
                using(var con= Connection)
                {
                    con.Open();
                    con.Execute
                    (
                        @"CREATE TABLE Tarefa
                        (
                            Id          integer primary key autoincrement,
                            Titulo      varchar(100) not null,
                            Descricao   varchar(100) not null,
                            Status      varchar(100) not null,
                            UsuarioId   integer,
                            FOREIGN KEY(UsuarioId) REFERENCES Usuario(Id)
                        )"
                    );
                    con.Execute
                    (
                         @"CREATE TABLE Usuario
                        (
                            Id          integer primary key autoincrement,
                            Email       varchar(100) not null,
                            Nome        varchar(100) not null,
                            Senha       varchar(30) not null,
                            Ativo       bool not null
                        )"
                    );
                    con.Execute
                    (
                         @"CREATE TABLE Agenda
                        (
                            Id          integer primary key autoincrement,
                            Titulo      varchar(100) not null,
                            Descricao   varchar(100) not null,
                            Data        varchar(100) not null,
                            Hora        varchar(100) not null,
                            Status      varchar(100) not null,
                            UsuarioId   integer,
                            FOREIGN KEY(UsuarioId) REFERENCES Usuario(Id)
                        )"
                    );

                    InsertDefaultData(con);
                }
            }
       }

       public void InsertDefaultData(SQLiteConnection con)
       {
            var usuario = new UsuarioDTO()
            {
                Email = "iury.fernandes@gmail.com",
                Nome = "Iury",
                Senha = "123456",
                Ativo = true
            };
            con.Execute
            (
                @"INSERT INTO Usuario
                (Email, Nome, Senha, Ativo) VALUES
                (@Email, @Nome, @Senha, @Ativo);", usuario
            );
       }
    }
}
