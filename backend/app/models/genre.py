from sqlalchemy import Column, Integer, String
from sqlalchemy.orm import relationship

from app.database.connection import Base

class Genre(Base):
    __tablename__ = "genre"

    id = Column(Integer, primary_key=True)
    name = Column(String, nullable=False)

    # RELAÇÕES
    mangas = relationship(
        "Manga",
        secondary="manga_genre",
        back_populates="genres"
    )