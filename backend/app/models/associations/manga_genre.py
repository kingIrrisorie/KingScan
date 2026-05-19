from sqlalchemy import Table, Column, Integer, ForeignKey

from app.database.connection import Base


manga_genre = Table(
    "manga_genre",
    Base.metadata,

    Column("manga_id", Integer, ForeignKey("manga.id")),
    Column("genre_id", Integer, ForeignKey("genre.id"))
)