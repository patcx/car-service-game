create table Garage
(
	GarageId uniqueidentifier not null,

	Name nvarchar(200) not null,
	Password nvarchar(200) not null,

	primary key(GarageId)
)

create table Worker
(
	WorkerId uniqueidentifier not null,
	GarageId uniqueidentifier null,

	Name nvarchar(200) not null,
	Efficiency int not null,
	Salary decimal not null,

	primary key(WorkerId),
	foreign key(GarageId) references Garage(GarageId)
)

create table RepairOrder
(
	RepairOrderId uniqueidentifier not null,
	CarName nvarchar(200) not null,
	RequiredWork int not null,
	Reward decimal not null,
	Description text null

	primary key(RepairOrderId),
)

create table RepairProcess
(
	RepairProcessId uniqueidentifier not null,
	GarageId uniqueidentifier not null,
	WorkerId uniqueidentifier not null,
	RepairOrderId uniqueidentifier unique not null,
	StallNumber int not null,
	CreatedDate datetime not null,
	IsPickedUp bit default(0),

	primary key(RepairProcessId),
	foreign key(GarageId) references Garage(GarageId),
	foreign key(WorkerId) references Worker(WorkerId),
	foreign key(RepairOrderId) references RepairOrder(RepairOrderId),

)