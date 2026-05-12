from sqlalchemy import Column, String, Integer, ForeignKey, Date
from sqlalchemy.orm import relationship
from app.database.connection import Base


class Manga(Base):
    __tablename__ = "manga"

    id = Column(Integer, primary_key=True)
    name = Column(String, nullable=False)
    status_id = Column(Integer, ForeignKey("status.id"))
    description = Column(String(500))
    release_date = Column(Date)

    # RELAÇOES
    status = relationship("Status", back_populates="mangas")
    authors = relationship("Author", secondary="manga_author")
    genres = relationship("Genre", secondary="manga_genre")
    chapters = relationship("Chapter", back_populates="manga")
