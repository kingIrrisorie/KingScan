from sqlalchemy import Column, String, Integer, ForeignKey, Date
from sqlalchemy.orm import relationship

from app.database.connection import Base

class Chapter(Base):
    __tablename__ = "chapter"

    id = Column(Integer, primary_key=True)
    manga = Column(Integer, ForeignKey)
    title = Column(String)
    number = Column(String)
    release_date = Column(Date)
    page = Column(Integer, ForeignKey)

    # RELACOES
    