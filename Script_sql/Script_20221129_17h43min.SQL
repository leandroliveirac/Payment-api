﻿CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "CATEGORIES" (
    "ID_CATEGORY" TEXT NOT NULL CONSTRAINT "PK_CATEGORIES" PRIMARY KEY,
    "DESCRIPTION" TEXT NOT NULL
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221116073354_Initial', '6.0.11');

COMMIT;

BEGIN TRANSACTION;

CREATE TABLE "ef_temp_CATEGORIES" (
    "ID_CATEGORY" TEXT NOT NULL CONSTRAINT "PK_CATEGORIES" PRIMARY KEY,
    "DESCRIPTION" TEXT NOT NULL,
    CONSTRAINT "AK_CATEGORIES_DESCRIPTION" UNIQUE ("DESCRIPTION")
);

INSERT INTO "ef_temp_CATEGORIES" ("ID_CATEGORY", "DESCRIPTION")
SELECT "ID_CATEGORY", "DESCRIPTION"
FROM "CATEGORIES";

COMMIT;

PRAGMA foreign_keys = 0;

BEGIN TRANSACTION;

DROP TABLE "CATEGORIES";

ALTER TABLE "ef_temp_CATEGORIES" RENAME TO "CATEGORIES";

COMMIT;

PRAGMA foreign_keys = 1;

BEGIN TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221117161054_Add descrption field from Categories table for alternate key', '6.0.11');

COMMIT;

BEGIN TRANSACTION;

CREATE UNIQUE INDEX "IX_CATEGORIES_DESCRIPTION" ON "CATEGORIES" ("DESCRIPTION");

CREATE TABLE "ef_temp_CATEGORIES" (
    "ID_CATEGORY" TEXT NOT NULL CONSTRAINT "PK_CATEGORIES" PRIMARY KEY,
    "DESCRIPTION" TEXT NOT NULL
);

INSERT INTO "ef_temp_CATEGORIES" ("ID_CATEGORY", "DESCRIPTION")
SELECT "ID_CATEGORY", "DESCRIPTION"
FROM "CATEGORIES";

COMMIT;

PRAGMA foreign_keys = 0;

BEGIN TRANSACTION;

DROP TABLE "CATEGORIES";

ALTER TABLE "ef_temp_CATEGORIES" RENAME TO "CATEGORIES";

COMMIT;

PRAGMA foreign_keys = 1;

BEGIN TRANSACTION;

CREATE UNIQUE INDEX "IX_CATEGORIES_DESCRIPTION" ON "CATEGORIES" ("DESCRIPTION");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221118121351_Mdifying the HasAlternteKey attribute for decription field indexes', '6.0.11');

COMMIT;

BEGIN TRANSACTION;

CREATE TABLE "PRODUCTS" (
    "ID_PRODUCT" TEXT NOT NULL CONSTRAINT "PK_PRODUCTS" PRIMARY KEY,
    "DESCRIPTION" TEXT NOT NULL,
    "PRICE" TEXT NOT NULL,
    "CategoryId" TEXT NOT NULL,
    CONSTRAINT "FK_PRODUCTS_CATEGORIES_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "CATEGORIES" ("ID_CATEGORY") ON DELETE CASCADE
);

CREATE INDEX "IX_PRODUCTS_CategoryId" ON "PRODUCTS" ("CategoryId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221118171500_add table Products', '6.0.11');

COMMIT;

BEGIN TRANSACTION;

ALTER TABLE "PRODUCTS" RENAME TO "TB_PRODUCTS";

ALTER TABLE "CATEGORIES" RENAME TO "TB_CATEGORIES";

DROP INDEX "IX_PRODUCTS_CategoryId";

CREATE INDEX "IX_TB_PRODUCTS_CategoryId" ON "TB_PRODUCTS" ("CategoryId");

DROP INDEX "IX_CATEGORIES_DESCRIPTION";

CREATE UNIQUE INDEX "IX_TB_CATEGORIES_DESCRIPTION" ON "TB_CATEGORIES" ("DESCRIPTION");

CREATE TABLE "TB_ORDER" (
    "ID_ORDER" TEXT NOT NULL CONSTRAINT "PK_TB_ORDER" PRIMARY KEY,
    "DATE" TEXT NOT NULL,
    "STATUS" INTEGER NOT NULL
);

CREATE TABLE "TB_SELLERS" (
    "ID_SELLER" TEXT NOT NULL CONSTRAINT "PK_TB_SELLERS" PRIMARY KEY,
    "NAME" TEXT NOT NULL,
    "DS_EMAIL" TEXT NOT NULL
);

CREATE TABLE "TB_ORDER_ITEMS" (
    "ID_ORDER_ITEM" TEXT NOT NULL CONSTRAINT "PK_TB_ORDER_ITEMS" PRIMARY KEY,
    "QUANTITY" INTEGER NOT NULL,
    "ID_PRODUCT" TEXT NOT NULL,
    "ID_ORDER" TEXT NOT NULL,
    CONSTRAINT "FK_TB_ORDER_ITEMS_TB_ORDER_ID_ORDER" FOREIGN KEY ("ID_ORDER") REFERENCES "TB_ORDER" ("ID_ORDER") ON DELETE CASCADE,
    CONSTRAINT "FK_TB_ORDER_ITEMS_TB_PRODUCTS_ID_PRODUCT" FOREIGN KEY ("ID_PRODUCT") REFERENCES "TB_PRODUCTS" ("ID_PRODUCT") ON DELETE CASCADE
);

CREATE TABLE "TB_PHONES" (
    "ID_PHONE" TEXT NOT NULL CONSTRAINT "PK_TB_PHONES" PRIMARY KEY,
    "DDD" TEXT NOT NULL,
    "NUMBER" TEXT NOT NULL,
    "TYPE" INTEGER NOT NULL,
    "ID_SELLER" TEXT NOT NULL,
    "SellerEntityId1" TEXT NULL,
    CONSTRAINT "FK_TB_PHONES_TB_SELLERS_ID_SELLER" FOREIGN KEY ("ID_SELLER") REFERENCES "TB_SELLERS" ("ID_SELLER") ON DELETE CASCADE,
    CONSTRAINT "FK_TB_PHONES_TB_SELLERS_SellerEntityId1" FOREIGN KEY ("SellerEntityId1") REFERENCES "TB_SELLERS" ("ID_SELLER")
);

CREATE TABLE "TB_SALES" (
    "ID_SALE" TEXT NOT NULL CONSTRAINT "PK_TB_SALES" PRIMARY KEY,
    "MOMENT" TEXT NOT NULL,
    "STATUS" INTEGER NOT NULL,
    "ID_SELLER" TEXT NOT NULL,
    "ID_ORDER" TEXT NOT NULL,
    CONSTRAINT "FK_TB_SALES_TB_ORDER_ID_SALE" FOREIGN KEY ("ID_SALE") REFERENCES "TB_ORDER" ("ID_ORDER") ON DELETE CASCADE,
    CONSTRAINT "FK_TB_SALES_TB_SELLERS_ID_SELLER" FOREIGN KEY ("ID_SELLER") REFERENCES "TB_SELLERS" ("ID_SELLER") ON DELETE CASCADE
);

CREATE INDEX "IX_TB_ORDER_ITEMS_ID_ORDER" ON "TB_ORDER_ITEMS" ("ID_ORDER");

CREATE INDEX "IX_TB_ORDER_ITEMS_ID_PRODUCT" ON "TB_ORDER_ITEMS" ("ID_PRODUCT");

CREATE INDEX "IX_TB_PHONES_ID_SELLER" ON "TB_PHONES" ("ID_SELLER");

CREATE INDEX "IX_TB_PHONES_SellerEntityId1" ON "TB_PHONES" ("SellerEntityId1");

CREATE INDEX "IX_TB_SALES_ID_SELLER" ON "TB_SALES" ("ID_SELLER");

CREATE TABLE "ef_temp_TB_PRODUCTS" (
    "ID_PRODUCT" TEXT NOT NULL CONSTRAINT "PK_TB_PRODUCTS" PRIMARY KEY,
    "CategoryId" TEXT NOT NULL,
    "DESCRIPTION" TEXT NOT NULL,
    "PRICE" TEXT NOT NULL,
    CONSTRAINT "FK_TB_PRODUCTS_TB_CATEGORIES_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "TB_CATEGORIES" ("ID_CATEGORY") ON DELETE CASCADE
);

INSERT INTO "ef_temp_TB_PRODUCTS" ("ID_PRODUCT", "CategoryId", "DESCRIPTION", "PRICE")
SELECT "ID_PRODUCT", "CategoryId", "DESCRIPTION", "PRICE"
FROM "TB_PRODUCTS";

CREATE TABLE "ef_temp_TB_CATEGORIES" (
    "ID_CATEGORY" TEXT NOT NULL CONSTRAINT "PK_TB_CATEGORIES" PRIMARY KEY,
    "DESCRIPTION" TEXT NOT NULL
);

INSERT INTO "ef_temp_TB_CATEGORIES" ("ID_CATEGORY", "DESCRIPTION")
SELECT "ID_CATEGORY", "DESCRIPTION"
FROM "TB_CATEGORIES";

COMMIT;

PRAGMA foreign_keys = 0;

BEGIN TRANSACTION;

DROP TABLE "TB_PRODUCTS";

ALTER TABLE "ef_temp_TB_PRODUCTS" RENAME TO "TB_PRODUCTS";

DROP TABLE "TB_CATEGORIES";

ALTER TABLE "ef_temp_TB_CATEGORIES" RENAME TO "TB_CATEGORIES";

COMMIT;

PRAGMA foreign_keys = 1;

BEGIN TRANSACTION;

CREATE INDEX "IX_TB_PRODUCTS_CategoryId" ON "TB_PRODUCTS" ("CategoryId");

CREATE UNIQUE INDEX "IX_TB_CATEGORIES_DESCRIPTION" ON "TB_CATEGORIES" ("DESCRIPTION");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221122192935_add new tables', '6.0.11');

COMMIT;

BEGIN TRANSACTION;

DROP INDEX "IX_TB_PHONES_SellerEntityId1";

ALTER TABLE "TB_ORDER" RENAME TO "TB_ORDERS";

CREATE TABLE "ef_temp_TB_ORDER_ITEMS" (
    "ID_ORDER_ITEM" TEXT NOT NULL CONSTRAINT "PK_TB_ORDER_ITEMS" PRIMARY KEY,
    "ID_ORDER" TEXT NOT NULL,
    "ID_PRODUCT" TEXT NOT NULL,
    "QUANTITY" INTEGER NOT NULL,
    CONSTRAINT "FK_TB_ORDER_ITEMS_TB_ORDERS_ID_ORDER" FOREIGN KEY ("ID_ORDER") REFERENCES "TB_ORDERS" ("ID_ORDER") ON DELETE CASCADE,
    CONSTRAINT "FK_TB_ORDER_ITEMS_TB_PRODUCTS_ID_PRODUCT" FOREIGN KEY ("ID_PRODUCT") REFERENCES "TB_PRODUCTS" ("ID_PRODUCT") ON DELETE CASCADE
);

INSERT INTO "ef_temp_TB_ORDER_ITEMS" ("ID_ORDER_ITEM", "ID_ORDER", "ID_PRODUCT", "QUANTITY")
SELECT "ID_ORDER_ITEM", "ID_ORDER", "ID_PRODUCT", "QUANTITY"
FROM "TB_ORDER_ITEMS";

CREATE TABLE "ef_temp_TB_PHONES" (
    "ID_PHONE" TEXT NOT NULL CONSTRAINT "PK_TB_PHONES" PRIMARY KEY,
    "DDD" TEXT NOT NULL,
    "ID_SELLER" TEXT NOT NULL,
    "NUMBER" TEXT NOT NULL,
    "TYPE" INTEGER NOT NULL,
    CONSTRAINT "FK_TB_PHONES_TB_SELLERS_ID_PHONE" FOREIGN KEY ("ID_PHONE") REFERENCES "TB_SELLERS" ("ID_SELLER") ON DELETE CASCADE,
    CONSTRAINT "FK_TB_PHONES_TB_SELLERS_ID_SELLER" FOREIGN KEY ("ID_SELLER") REFERENCES "TB_SELLERS" ("ID_SELLER") ON DELETE CASCADE
);

INSERT INTO "ef_temp_TB_PHONES" ("ID_PHONE", "DDD", "ID_SELLER", "NUMBER", "TYPE")
SELECT "ID_PHONE", "DDD", "ID_SELLER", "NUMBER", "TYPE"
FROM "TB_PHONES";

CREATE TABLE "ef_temp_TB_SALES" (
    "ID_SALE" TEXT NOT NULL CONSTRAINT "PK_TB_SALES" PRIMARY KEY,
    "ID_ORDER" TEXT NOT NULL,
    "ID_SELLER" TEXT NOT NULL,
    "MOMENT" TEXT NOT NULL,
    "STATUS" INTEGER NOT NULL,
    CONSTRAINT "FK_TB_SALES_TB_ORDERS_ID_SALE" FOREIGN KEY ("ID_SALE") REFERENCES "TB_ORDERS" ("ID_ORDER") ON DELETE CASCADE,
    CONSTRAINT "FK_TB_SALES_TB_SELLERS_ID_SELLER" FOREIGN KEY ("ID_SELLER") REFERENCES "TB_SELLERS" ("ID_SELLER") ON DELETE CASCADE
);

INSERT INTO "ef_temp_TB_SALES" ("ID_SALE", "ID_ORDER", "ID_SELLER", "MOMENT", "STATUS")
SELECT "ID_SALE", "ID_ORDER", "ID_SELLER", "MOMENT", "STATUS"
FROM "TB_SALES";

CREATE TABLE "ef_temp_TB_ORDERS" (
    "ID_ORDER" TEXT NOT NULL CONSTRAINT "PK_TB_ORDERS" PRIMARY KEY,
    "DATE" TEXT NOT NULL,
    "STATUS" INTEGER NOT NULL
);

INSERT INTO "ef_temp_TB_ORDERS" ("ID_ORDER", "DATE", "STATUS")
SELECT "ID_ORDER", "DATE", "STATUS"
FROM "TB_ORDERS";

COMMIT;

PRAGMA foreign_keys = 0;

BEGIN TRANSACTION;

DROP TABLE "TB_ORDER_ITEMS";

ALTER TABLE "ef_temp_TB_ORDER_ITEMS" RENAME TO "TB_ORDER_ITEMS";

DROP TABLE "TB_PHONES";

ALTER TABLE "ef_temp_TB_PHONES" RENAME TO "TB_PHONES";

DROP TABLE "TB_SALES";

ALTER TABLE "ef_temp_TB_SALES" RENAME TO "TB_SALES";

DROP TABLE "TB_ORDERS";

ALTER TABLE "ef_temp_TB_ORDERS" RENAME TO "TB_ORDERS";

COMMIT;

PRAGMA foreign_keys = 1;

BEGIN TRANSACTION;

CREATE INDEX "IX_TB_ORDER_ITEMS_ID_ORDER" ON "TB_ORDER_ITEMS" ("ID_ORDER");

CREATE INDEX "IX_TB_ORDER_ITEMS_ID_PRODUCT" ON "TB_ORDER_ITEMS" ("ID_PRODUCT");

CREATE INDEX "IX_TB_PHONES_ID_SELLER" ON "TB_PHONES" ("ID_SELLER");

CREATE INDEX "IX_TB_SALES_ID_SELLER" ON "TB_SALES" ("ID_SELLER");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221122195837_alter phone table', '6.0.11');

COMMIT;

BEGIN TRANSACTION;

CREATE TABLE "ef_temp_TB_PHONES" (
    "ID_PHONE" TEXT NOT NULL CONSTRAINT "PK_TB_PHONES" PRIMARY KEY,
    "DDD" TEXT NOT NULL,
    "ID_SELLER" TEXT NOT NULL,
    "NUMBER" TEXT NOT NULL,
    "TYPE" INTEGER NOT NULL,
    CONSTRAINT "FK_TB_PHONES_TB_SELLERS_ID_SELLER" FOREIGN KEY ("ID_SELLER") REFERENCES "TB_SELLERS" ("ID_SELLER") ON DELETE CASCADE
);

INSERT INTO "ef_temp_TB_PHONES" ("ID_PHONE", "DDD", "ID_SELLER", "NUMBER", "TYPE")
SELECT "ID_PHONE", "DDD", "ID_SELLER", "NUMBER", "TYPE"
FROM "TB_PHONES";

COMMIT;

PRAGMA foreign_keys = 0;

BEGIN TRANSACTION;

DROP TABLE "TB_PHONES";

ALTER TABLE "ef_temp_TB_PHONES" RENAME TO "TB_PHONES";

COMMIT;

PRAGMA foreign_keys = 1;

BEGIN TRANSACTION;

CREATE INDEX "IX_TB_PHONES_ID_SELLER" ON "TB_PHONES" ("ID_SELLER");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221125175602_alter column FK phone table', '6.0.11');

COMMIT;

BEGIN TRANSACTION;

CREATE INDEX "IX_TB_SALES_ID_ORDER" ON "TB_SALES" ("ID_ORDER");

CREATE TABLE "ef_temp_TB_SALES" (
    "ID_SALE" TEXT NOT NULL CONSTRAINT "PK_TB_SALES" PRIMARY KEY,
    "ID_ORDER" TEXT NOT NULL,
    "ID_SELLER" TEXT NOT NULL,
    "MOMENT" TEXT NOT NULL,
    "STATUS" INTEGER NOT NULL,
    CONSTRAINT "FK_TB_SALES_TB_ORDERS_ID_ORDER" FOREIGN KEY ("ID_ORDER") REFERENCES "TB_ORDERS" ("ID_ORDER") ON DELETE CASCADE,
    CONSTRAINT "FK_TB_SALES_TB_SELLERS_ID_SELLER" FOREIGN KEY ("ID_SELLER") REFERENCES "TB_SELLERS" ("ID_SELLER") ON DELETE CASCADE
);

INSERT INTO "ef_temp_TB_SALES" ("ID_SALE", "ID_ORDER", "ID_SELLER", "MOMENT", "STATUS")
SELECT "ID_SALE", "ID_ORDER", "ID_SELLER", "MOMENT", "STATUS"
FROM "TB_SALES";

CREATE TABLE "ef_temp_TB_PHONES" (
    "ID_PHONE" TEXT NOT NULL CONSTRAINT "PK_TB_PHONES" PRIMARY KEY,
    "DDD" TEXT NOT NULL,
    "ID_SELLER" TEXT NOT NULL,
    "NUMBER" TEXT NOT NULL,
    "TYPE" TEXT NOT NULL,
    CONSTRAINT "FK_TB_PHONES_TB_SELLERS_ID_SELLER" FOREIGN KEY ("ID_SELLER") REFERENCES "TB_SELLERS" ("ID_SELLER") ON DELETE CASCADE
);

INSERT INTO "ef_temp_TB_PHONES" ("ID_PHONE", "DDD", "ID_SELLER", "NUMBER", "TYPE")
SELECT "ID_PHONE", "DDD", "ID_SELLER", "NUMBER", "TYPE"
FROM "TB_PHONES";

COMMIT;

PRAGMA foreign_keys = 0;

BEGIN TRANSACTION;

DROP TABLE "TB_SALES";

ALTER TABLE "ef_temp_TB_SALES" RENAME TO "TB_SALES";

DROP TABLE "TB_PHONES";

ALTER TABLE "ef_temp_TB_PHONES" RENAME TO "TB_PHONES";

COMMIT;

PRAGMA foreign_keys = 1;

BEGIN TRANSACTION;

CREATE INDEX "IX_TB_SALES_ID_ORDER" ON "TB_SALES" ("ID_ORDER");

CREATE INDEX "IX_TB_SALES_ID_SELLER" ON "TB_SALES" ("ID_SELLER");

CREATE INDEX "IX_TB_PHONES_ID_SELLER" ON "TB_PHONES" ("ID_SELLER");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221129204023_Teste', '6.0.11');

COMMIT;

