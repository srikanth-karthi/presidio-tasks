

# Working with Docker and PostgreSQL

This guide will walk you through the steps to pull a PostgreSQL image, start a container, connect to the PostgreSQL database within the Docker container using `psql`, and create a new database.

## 1. Pull PostgreSQL Image

To pull the latest PostgreSQL image from Docker Hub, run the following command:

```sh
docker pull postgres
```

## 2. Start a Container

To start a PostgreSQL container, run the following command. This will create a container named `postgres-container`, set the `POSTGRES_PASSWORD` to `mysecretpassword`, and map port 5432 of the container to port 5432 on your host machine.

```sh
docker run --name postgres-container -e POSTGRES_PASSWORD=mysecretpassword -d -p 5432:5432 postgres
```

## 3. Connect to the PostgreSQL Database within the Docker Container using `psql`

To connect to the running PostgreSQL container, use the following command. This will open a `psql` session as the `postgres` user.

```sh
docker exec -it postgres-container psql -U postgres
```

## 4. Create a Database

Within the `psql` session, create a new database named `sample_db` with the following command:

```sql
CREATE DATABASE sample_db;
```

## 5. List All Databases

To see a list of all databases, use the `\l` command:

```sql
\l
```

This will display a list of all databases, including the newly created `sample_db`.

