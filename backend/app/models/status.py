from sqlalchemy import Column, Integer, String
from sqlalchemy.orm import relationship
from app.database.connection import Base

class Status(Base):
    __tablename__ = "status"

    id = Column(Integer, primary_key=True)
    name = Column(String, nullable=False)

    # RELAÇÕES
    mangas = relationship(
        "Manga",
        back_populates="status"
    )