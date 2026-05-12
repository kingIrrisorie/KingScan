from sqlalchemy import Column, Integer, String, ForeignKey

from app.database.connection import Base

class Genre(Base):
    __tablename__ = "genre"

    id = Column(Integer, primary_key=True)
    name = Column(String)
    manga_id = Column(Integer, ForeignKey)
    