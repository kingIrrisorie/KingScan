from sqlalchemy import Column, String, Integer, ForeignKey, Date
from sqlalchemy.orm import relationship
from app.database.connection import Base


class Manga(Base):
    __tablename__ = "manga"

    id = Column(Integer, primary_key=True)
    title = Column(String, nullable=False)
    status_id = Column(Integer, ForeignKey("status.id"))
    description = Column(String(500))
    release_date = Column(Date)
    thumbnail_url = Column(String)

    # =========================
    # RELACIONAMENTOS
    # =========================

    # 1:N -> Manga -> Status
    status = relationship(
        "Status",
        back_populates="mangas"
    )

    # N:N -> Manga <-> Author
    authors = relationship(
        "Author",
        secondary="manga_author",
        back_populates="mangas"
    )

    # N:N -> Manga <-> Genre
    genres = relationship(
        "Genre",
        secondary="manga_genre",
        back_populates="mangas"
    )

    # 1:N -> Manga -> Chapter
    chapters = relationship(
        "Chapter",
        back_populates="manga"
    )