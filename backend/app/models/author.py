from sqlalchemy import Column, Integer, String, ForeignKey
from sqlalchemy.orm import relationship
from app.database.connection import Base

class Author(Base):
    __tablename__ = "author"

    id = Column(Integer, primary_key=True)
    name = Column(String)
    manga = Column(Integer, ForeignKey("manga.id"))

    # RELAÇOES
    