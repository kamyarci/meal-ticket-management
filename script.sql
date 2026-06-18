CREATE DATABASE "mt-management";

CREATE TABLE employees (
                           "Id" uuid NOT NULL,
                           "Name" varchar(100) NOT NULL,
                           "Cpf" varchar(11) NOT NULL,
                           "Status" varchar(1) NOT NULL,
                           "UpdatedAt" timestamptz NOT NULL,
                           CONSTRAINT "PK_employees" PRIMARY KEY ("Id")
);

CREATE UNIQUE INDEX "IX_employees_Cpf" ON employees ("Cpf");

CREATE TABLE meal_tickets (
                              "Id" uuid NOT NULL,
                              "EmployeeId" uuid NOT NULL,
                              "Quantity" integer NOT NULL,
                              "DeliveredAt" timestamptz NOT NULL,
                              "Status" varchar(1) NOT NULL,
                              "UpdatedAt" timestamptz NOT NULL,
                              CONSTRAINT "PK_meal_tickets" PRIMARY KEY ("Id"),
                              CONSTRAINT "FK_meal_tickets_employees_EmployeeId"
                                  FOREIGN KEY ("EmployeeId") REFERENCES employees ("Id") ON DELETE RESTRICT
);

CREATE INDEX "IX_meal_tickets_EmployeeId" ON meal_tickets ("EmployeeId");