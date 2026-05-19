from sqlalchemy import Column, Integer, String
from sqlalchemy.orm import relationship
from app.database.connection import Base

class Author(Base):
    __tablename__ = "author"

    id = Column(Integer, primary_key=True)
    name = Column(String, nullable=False)

    # RELAÇÕES
    mangas = relationship(
        "Manga",
        secondary="manga_author",
        back_populates="authors"
    )