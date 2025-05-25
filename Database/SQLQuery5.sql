--   1: Create the Database
CREATE DATABASE MISHADB;
GO
drop DATABASE MISHADB;

USE MISHADB;
GO

-- Create the Roles Table
CREATE TABLE Roles (
    RoleId INT PRIMARY KEY IDENTITY,
    RoleName NVARCHAR(50) NOT NULL UNIQUE -- Examples: 'Admin', 'User'
);

-- Create the Users Table
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL, 
    RoleId INT NOT NULL, -- Reference to Roles
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);

-- Create the Categories Table
CREATE TABLE Categories (
    CategoryId INT PRIMARY KEY IDENTITY,
    CategoryName NVARCHAR(100) NOT NULL UNIQUE -- Examples: 'Action', 'Puzzle'
);

-- Create the Products Table
CREATE TABLE Products (
    ProductId INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    Price DECIMAL(10, 2) NOT NULL,
    ImagePath NVARCHAR(255), -- Path or URL to product image
    ReleaseDate DATE,
    CreatedDate DATETIME DEFAULT GETDATE(),
    CategoryId INT, -- Reference to Categories table
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
);

-- Create the Orders Table
CREATE TABLE Orders (
    OrderId INT PRIMARY KEY IDENTITY,
    UserId INT NOT NULL, -- Reference to the user who placed the order
    TotalAmount DECIMAL(10, 2) NOT NULL, -- Total cost of the order
    OrderDate DATETIME DEFAULT GETDATE(), -- Date when the order was placed
    FOREIGN KEY (UserId) REFERENCES Users(UserId) -- Links to the Users table
);

-- Create the OrderDetails Table
CREATE TABLE OrderDetails (
    OrderDetailId INT PRIMARY KEY IDENTITY,
    OrderId INT NOT NULL, -- Reference to the order
    ProductId INT NOT NULL, -- Reference to the product
    Price DECIMAL(10, 2) NOT NULL, -- Price of the product at the time of purchase
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

-- Create the ShoppingCart Table
CREATE TABLE ShoppingCart (
    CartId INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(50) NOT NULL,
    ProductID INT NOT NULL,
    FOREIGN KEY (Username) REFERENCES Users(Username),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductId)
);




--  Insert Sample Data into Roles Table
INSERT INTO Roles (RoleName) 
VALUES ('Admin'), ('User');

--  Insert Sample Data into Users Table
INSERT INTO Users (Username, Email, PasswordHash, RoleId) 
VALUES 
('AdminUser', 'admin@example.com', 'hashed_password_1', 1), 
('NormalUser', 'user@example.com', 'hashed_password_2', 2),
('User1', 'user1@example.com', 'hashed_password_3', 2), 
('User2', 'user2@example.com', 'hashed_password_4', 2),
('User3', 'user3@example.com', 'hashed_password_5', 2),
('User4', 'user4@example.com', 'hashed_password_6', 2),
('User5', 'user5@example.com', 'hashed_password_7', 2),
('User6', 'user6@example.com', 'hashed_password_8', 2),
('User7', 'user7@example.com', 'hashed_password_9', 2),
('User8', 'user8@example.com', 'hashed_password_10', 2);

INSERT INTO Categories (CategoryName) 
VALUES 
('Action'), 
('Adventure'), 
('Puzzle'), 
('RPG'), 
('Shooter'), 
('Strategy'), 
('Sports'), 
('Simulation'), 
('Platformer'), 
('Horror'), 
('Fighting'), 
('Racing'), 
('MMORPG'), 
('Casual'), 
('Card Game'), 
('Trivia'), 
('Sandbox'), 
('Survival'), 
('Stealth'), 
('Music');




--  Insert Sample Data into Products Table
INSERT INTO Products (Name, Description, Price, ImagePath, ReleaseDate, CategoryId)
VALUES
-- --RPGs (CategoryId = 4)
--('Disco Elysium: Final Cut', 'An award-winning role-playing game', 39.99, '/Images/Disco_elysium_Final_Cut.jpg', '2021-03-30', 4),
--('Dark Souls: Remastered Edition', 'A challenging action RPG with stunning visuals', 49.99, '/Images/Dark_Souls_RE.jpg', '2018-05-25', 4),
--('The Witcher 3: Wild Hunt', 'An open-world RPG with captivating storylines', 59.99, '/Images/Witcher3.jpg', '2015-05-19', 4),
--('Elden Ring', 'A critically acclaimed open-world action RPG', 69.99, '/Images/Elden_Ring.jpg', '2022-02-25', 4),
--('Final Fantasy XVI', 'The latest installment in the beloved RPG series', 79.99, '/Images/Final_Fantasy_XVI.jpg', '2023-06-22', 4),

---- Action Games (CategoryId = 1)
--('Assassin’s Creed Valhalla', 'A Viking-inspired open-world action game', 49.99, '/Images/AC_Valhalla.jpg', '2020-11-10', 1),
--('God of War Ragnarök', 'An epic Norse mythology action-adventure', 69.99, '/Images/God_of_War.jpg', '2022-11-09', 1),
--('Spider-Man: Miles Morales', 'A thrilling superhero action game', 39.99, '/Images/SpiderMan_MM.jpg', '2020-11-12', 1),
--('Horizon Forbidden West', 'A breathtaking action-adventure in a dystopian world', 59.99, '/Images/Horizon_FW.jpg', '2022-02-18', 1),
--('Sekiro: Shadows Die Twice', 'A challenging samurai action-adventure game', 49.99, '/Images/Sekiro.jpg', '2019-03-22', 1),

---- Survival (CategoryId = 18) 
--('S.T.A.L.K.E.R.: Shadow of Chernobyl', 'A gripping survival horror set in the Chernobyl Exclusion Zone', 19.99, '/Images/STALKER_Shadow_of_Chernobyl.jpg', '2007-03-20', 18),

---- Adventure (CategoryId = 2)
--('Red Dead Redemption 2', 'An immersive Western adventure', 49.99, '/Images/RDR2.jpg', '2018-10-26', 2),
--('Uncharted: Legacy of Thieves Collection', 'A remastered collection of thrilling Uncharted adventures', 49.99, '/Images/Uncharted_Legacy_of_Thieves.jpg', '2022-01-28', 2),
--('Tomb Raider: Definitive Edition', 'A gripping origin story of Lara Croft', 29.99, '/Images/Tomb_Raider.jpg', '2014-01-28', 2),
--('Life is Strange', 'A heartfelt episodic adventure game', 19.99, '/Images/Life_Is_Strange.jpg', '2015-01-30', 2),

-- --Puzzle (CategoryId = 3)
--('Portal 2', 'A mind-bending physics-based puzzle game', 14.99, '/Images/Portal2.jpg', '2011-04-19', 3),
--('The Witness', 'A beautiful and challenging puzzle game', 19.99, '/Images/The_Witness.jpg', '2016-01-26', 3),
--('Tetris Effect', 'An immersive and musical puzzle game', 29.99, '/Images/Tetris_Effect.jpg', '2018-11-09', 3),
--('Baba Is You', 'A creative and innovative puzzle game', 9.99, '/Images/Baba_Is_You.jpg', '2019-03-13', 3),
--('INSIDE', 'A dark, atmospheric puzzle-platformer', 14.99, '/Images/Inside.jpg', '2016-07-07', 3),

---- Shooters (CategoryId = 5)
--('Call of Duty: Modern Warfare II', 'A high-octane first-person shooter', 69.99, '/Images/CoD_MW2.jpg', '2022-10-28', 5),
--('Halo Infinite', 'An epic sci-fi shooter adventure', 49.99, '/Images/Halo_Infinite.jpg', '2021-12-08', 5),
--('Doom Eternal', 'A fast-paced demon-slaying shooter', 39.99, '/Images/Doom_Eternal.jpg', '2020-03-20', 5),
--('Overwatch 2', 'A popular team-based shooter', 19.99, '/Images/Overwatch2.jpg', '2022-10-04', 5),
--('Battlefield 2042', 'A large-scale multiplayer shooter', 59.99, '/Images/Battlefield_2042.jpg', '2021-11-19', 5),

---- Sports (CategoryId = 7)
--('Rocket League', 'A fast-paced soccer game with cars', 19.99, '/Images/Rocket_League.jpg', '2015-07-07', 7),
--('PGA Tour 2K23', 'A premier golf simulation game', 49.99, '/Images/PGA_Tour_2K23.jpg', '2022-10-14', 7),
--('F1 22', 'An immersive Formula One racing simulation', 59.99, '/Images/F1_22.jpg', '2022-06-28', 7),
--('FIFA 22', 'A realistic and immersive soccer simulation game', 59.99, '/Images/FIFA_22.jpg', '2021-10-01', 7),
--('Skate 3', 'A classic skateboarding simulation', 29.99, '/Images/Skate3.jpg', '2010-05-11', 7),

-- --Fighting Games (CategoryId = 10)
--('Street Fighter V', 'An iconic fighting game', 29.99, '/Images/Street_Fighter_V.jpg', '2016-02-16', 10),
--('Tekken 7', 'A classic 3D fighting game', 39.99, '/Images/Tekken_7.jpg', '2017-06-02', 10),
--('Mortal Kombat 11', 'A thrilling and violent fighting game', 49.99, '/Images/MK11.jpg', '2019-04-23', 10),
--('Dragon Ball FighterZ', 'An anime-inspired fighting game', 59.99, '/Images/DB_FighterZ.jpg', '2018-01-26', 10),
--('Guilty Gear -Strive-', 'A beautifully animated fighting game', 49.99, '/Images/Guilty_Gear_Strive.jpg', '2021-06-11', 10),

-- --Racing (CategoryId = 12)
--('Forza Horizon 5', 'A breathtaking open-world racing game', 59.99, '/Images/Forza_Horizon_5.jpg', '2021-11-09', 12),
--('Need for Speed: Heat', 'A fast-paced street racing game', 49.99, '/Images/NFS_Heat.jpg', '2019-11-08', 12),
--('Need for Speed Unbound', 'A vibrant and thrilling street racing game', 69.99, '/Images/NFS_Unbound.jpg', '2022-12-02', 12),
--('Assetto Corsa', 'A racing simulation for enthusiasts', 39.99, '/Images/Assetto_Corsa.jpg', '2014-12-19', 12),
--('Burnout Paradise Remastered', 'A remaster of the classic arcade racer', 29.99, '/Images/Burnout_Paradise.jpg', '2018-08-21', 12),

---- Sandbox (CategoryId = 17)
--('Garry’s Mod', 'A physics-based sandbox game with endless creativity', 9.99, '/Images/Garrys_Mod.jpg', '2006-11-29', 17),
--('Terraria', 'A 2D sandbox adventure game', 14.99, '/Images/Terraria.jpg', '2011-05-16', 17),
--('No Man’s Sky', 'An open-world space exploration game', 49.99, '/Images/No_Mans_Sky.jpg', '2016-08-09', 17),
--('ARK: Survival Evolved', 'A dinosaur survival sandbox game', 29.99, '/Images/ARK_Survival.jpg', '2017-08-29', 17),
--('Rust', 'A brutal survival sandbox game', 39.99, '/Images/Rust.jpg', '2018-02-08', 17),

-- Casual (CategoryId = 14)
('Among Us', 'A fun multiplayer social game', 4.99, '/Images/Among_Us.jpg', '2018-11-16', 14),
('Stardew Valley', 'A relaxing farming and life simulation game', 14.99, '/Images/Stardew_Valley.jpg', '2016-02-26', 14),
('Goat Simulator', 'A hilarious goat simulation game', 9.99, '/Images/Goat_Simulator.jpg', '2014-04-01', 14),
('The Jackbox Party Pack', 'A collection of party games', 24.99, '/Images/Jackbox.jpg', '2014-11-26', 14),
('Overcooked! 2', 'A frantic cooperative cooking game', 24.99, '/Images/Overcooked2.jpg', '2018-08-07', 14);

--  Insert Sample Data into OrderDetails Table
INSERT INTO OrderDetails (OrderId, ProductId, Price)
VALUES 
(1, 1, 19.99), 
(1, 2, 29.99), 
(1, 1, 19.99), (1, 2, 29.99),
(2, 3, 14.99), (2, 4, 29.99),
(3, 5, 39.99),
(4, 6, 29.99), (4, 7, 29.99),
(5, 8, 19.99),
(6, 9, 24.99), (6, 10, 24.99), (6, 11, 24.99),
(7, 12, 29.99),
(8, 13, 29.99), (8, 14, 29.99),
(9, 15, 49.99), (9, 16, 49.99),
(10, 17, 24.99), (10, 18, 19.99),
(11, 19, 39.99), (11, 20, 39.99),
(12, 21, 24.99), (12, 22, 39.99),
(13, 23, 59.99), (13, 24, 59.99),
(14, 25, 49.99),
(15, 26, 39.99), (15, 27, 39.99),
(16, 28, 24.99),
(17, 29, 39.99), (17, 30, 39.99), (17, 31, 49.99),
(18, 32, 39.99), (18, 33, 39.99),
(19, 34, 19.99),
(20, 35, 49.99);

-- Insert Sample Data into Orders Table
INSERT INTO Orders (UserId, TotalAmount)
VALUES (1, 49.98), -- Assume UserId 1 places an order
(1, 49.98),   -- Order 1 by User1
(2, 45.97),   -- Order 2 by User2
(3, 39.99),   -- Order 3 by User3
(4, 59.98),   -- Order 4 by User4
(5, 19.99),   -- Order 5 by User5
(6, 74.97),   -- Order 6 by User6
(7, 29.99),   -- Order 7 by User7
(8, 59.99),   -- Order 8 by User8
(9, 99.98),   -- Order 9 by User9
(10, 44.97),  -- Order 10 by User10
(1, 84.97),   -- Order 11 by User1
(2, 64.97),   -- Order 12 by User2
(3, 119.96),  -- Order 13 by User3
(4, 49.99),   -- Order 14 by User4
(5, 39.99),   -- Order 15 by User5
(6, 24.99),   -- Order 16 by User6
(7, 129.96),  -- Order 17 by User7
(8, 79.97),   -- Order 18 by User8
(9, 19.99),   -- Order 19 by User9
(10, 49.99);  -- Order 20 by User10

-- Controll
ALTER TABLE Users
ADD CONSTRAINT DF_RoleId_Default
DEFAULT 2 FOR RoleId;


SELECT * FROM Products;
SELECT ProductId, Name, Description, Price, ImagePath FROM Products;
ALTER TABLE ShoppingCart RENAME COLUMN UserEmail TO Username;

DROP TABLE IF EXISTS OrderDetails;
DROP TABLE IF EXISTS Orders;
DROP TABLE IF EXISTS ShoppingCart;
DROP TABLE IF EXISTS Products;
DROP TABLE IF EXISTS Categories;
DROP TABLE IF EXISTS Users;
DROP TABLE IF EXISTS Roles;

DELETE FROM OrderDetails;
DELETE FROM Orders;
DELETE FROM ShoppingCart;
DELETE FROM Products;
DELETE FROM Categories;
DELETE FROM Users;
DELETE FROM Roles;


